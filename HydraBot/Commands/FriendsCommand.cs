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
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            var helper = new UsersHelper();
            var kb = new KeyboardBuilder(bot);
            var text = $"🧒 Друзья пользователя  {helper.GetLink(user)}";
            if (user.Friends is null)
            {
                text = "💔 Ваш список друзей пуст. Найдите себе друзей скорее!";
                kb.AddButton("🔙 В меню телефона", "menuphone");
            }
            else
            {
                var friends = FriendsHelper.GetFriends(user.Friends);
                foreach (var friendId in friends)
                {
                    var friend = Main.Api.Users.GetUserFromId(friendId);
                    text += $"\n 🧒 [{friend.Id}]| [{friend.Prefix}] {helper.GetLink(friend)} - {friend.Level} уровень.";
                }
            
                kb.AddButton("➕ Добавить друга", "addfriend");
                kb.AddLine();
                kb.AddButton("❌ Удалить друга", "removefriend");
                kb.AddLine();
                kb.AddButton("✔ Запросы в друзья", "requestfriends");
                kb.AddLine();
                kb.AddButton("↩ В меню телефона", "menuphone");
            }
            
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "friends";
        public string[] Aliases => new string[0];
    }
}