using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Friends
{
    public class AcceptRequestFriendCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            var request = msg.Payload.Arguments[0].ToLong();
            var u = Main.Api.Users.GetUser(msg);
            using (var db = new Database())
            {
                var user = db.Users.Single(uu => uu.Id == u.Id);
                var requests = FriendsHelper.GetFriends(user.FriendsRequests);
                requests.Remove(request);
                var s = string.Empty;
                foreach (var rq in requests)
                {
                    s += $"{rq};";
                }

                user.FriendsRequests = s;
                user.Friends += $"{request};";
                var userFriend = db.Users.Single(uu => uu.Id == request);
                userFriend.Friends += $"{user.Id};";
                
                db.SaveChanges();
            }
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("📱 В меню телефона", "menuphone");
            
            sender.Text("Пользователь добавлен в список Ваших друзей!", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "acceptrequestfriend";
        public string[] Aliases => new string[0];
    }
}