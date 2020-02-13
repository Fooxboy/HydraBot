using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class UnBanCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var api = Main.Api;
            var userSend = api.Users.GetUser(msg);
            if (userSend.Access < 6)
            {
                sender.Text("❌ Вам недоступна эта команда!", msg.ChatId);
                return;
            }


            var id = msg.Text.Split(" ")[1].ToLong();

            using (var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == id);
                user.IsBanned = false;
                user.TimeBan = 0;
                db.SaveChanges();
                sender.Text("✔ Вы были разблокированы", user.VkId);
            }
            
            sender.Text("Пользователь разблокирован", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "unban";
        public string[] Aliases => new string[] {"разбан"};
    }
}