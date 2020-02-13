using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Friends
{
    public class RemoveFriendCommand:INucleusCommand
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
            UsersCommandHelper.GetHelper().Add("removefriend", user.Id);
            sender.Text("❓ Укажите Id (в боте) друга, которого Вы хотите удалить со списка Ваших друзей.", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }


        public static string RemoveFriend(User cretor, long userId)
        {
            var friends = FriendsHelper.GetFriends(cretor.Friends);

            if (friends.All(id => id != userId)) return "❌ Пользователя с таким Id нет в Ваших друзьях.";

            using (var db = new Database())
            {
                var crt = db.Users.Single(u => u.Id == cretor.Id);
                var user = db.Users.Single(u => u.Id == userId);


                var fr1 = FriendsHelper.GetFriends(crt.Friends);
                var fr2 = FriendsHelper.GetFriends(user.Friends);

                fr1.Remove(userId);
                fr2.Remove(cretor.Id);
                var stringFriends1 = string.Empty;
                var stringFriends2 = string.Empty;

                foreach (var frs1 in fr1) stringFriends1 += $"{frs1};";
                foreach (var frs2 in fr2) stringFriends2 += $"{frs2};";

                crt.Friends = stringFriends1;
                user.Friends = stringFriends2;

                db.SaveChanges();

            }

            return "✔ Пользователь удален из списка ваших друзей.";
        }

        public string Command => "removefriend";
        public string[] Aliases => new string[0];
    }
}