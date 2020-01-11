using System.Collections.Generic;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands.Friends
{
    public class RequestFriendsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            if (user.FriendsRequests == "")
            {
                sender.Text("🤔 У Вас нет запросов в друзья", msg.ChatId, kb.Build());
                return;
            }
            var requests = FriendsHelper.GetFriends(user.FriendsRequests);

            var text = "🧒 Запросы в друзья";

            foreach (var request in requests)
            {
                var req = Main.Api.Users.GetUserFromId(request);
                text += $"\n🧒 {req.Id} | [{req.Prefix}] {req.Name} - {req.Level} уровень.";
            }

            kb.AddButton("✔ Принять запрос", "acceptrequestfriend", new List<string>(){requests[0].ToString()});
            kb.AddLine();
            kb.AddButton("❌ Отклонить запрос", "noacceptrequestfriend", new List<string>(){requests[0].ToString()});
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "requestfriends";
        public string[] Aliases => new string[0];
    }
}