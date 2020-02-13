using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class BanCommand :INucleusCommand
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
            
            var array = msg.Text.Split(" ");

            var userId = array[1].ToLong();
            var time = array[2].ToLong();

            using (var db = new Database())
            {
                var usr = db.Users.Single(u => u.Id == userId);
                usr.IsBanned = true;
                usr.TimeBan = time;
                db.SaveChanges();
            }
            
            sender.Text("Бан выдан.", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "ban";
        public string[] Aliases => new string[] {"бан"};
    }
}