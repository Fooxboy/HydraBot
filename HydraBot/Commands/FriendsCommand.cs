using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands
{
    public class FriendsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var friends = FriendsHelper.GetFriends(user.Friends);
            var helper = new UsersHelper();
            var text = $"🧒 Друзья пользователя  {helper.GetLink(user)}";

            foreach (var friendId in friends)
            {
                var friend = Main.Api.Users.GetUserFromId(friendId);
                text += $"\n 🧒 [{friend.Id}]| [{friend.Prefix}] {helper.GetLink(friend)} - {friend.Level} уровень.";
            }
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("➕ Добавить друга", "addfriend");
            kb.AddLine();
            kb.AddButton("❌ Удалить друга", "removefriend");
            kb.AddLine();
            kb.AddButton("✔ Запросы в друзья", "requestfriends");
            kb.AddLine();
            kb.AddButton("↩ В меню телефона", "menuphone");
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "friends";
        public string[] Aliases => new string[0];
    }
}