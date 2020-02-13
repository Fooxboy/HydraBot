using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class BansCommand:INucleusCommand
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
            
            long userId;
            var api = Main.Api;
            var userSend = api.Users.GetUser(msg);
            if (userSend.Access < 6)
            {
                sender.Text("❌ Вам недоступна эта команда!", msg.ChatId);
                return;
            }

            var text = string.Empty;

            text = "☠ Пользователи, которые сейчас находятся в бане:";

            var helper = new UsersHelper();
            
            using (var db = new Database())
            {
                var users = db.Users.Where(u => u.IsBanned);
                foreach (var user in users)
                {
                    text += $"\n {helper.GetLink(user)} - {user.TimeBan} часов.";
                }
            }
            
            sender.Text(text, msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "bans";
        public string[] Aliases => new string[] {"баны"};
    }
}