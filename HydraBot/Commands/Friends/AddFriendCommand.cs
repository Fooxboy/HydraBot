using System.Linq;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using VkNet.Exception;

namespace HydraBot.Commands.Friends
{
    public class AddFriendCommand:INucleusCommand
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
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("addfriend", user.Id);
            sender.Text("❓ Укажите Id (в боте) друга, мы отправим ему запрос.", msg.ChatId);
        }


        public static string AddFriend(User creator, long userId, IMessageSenderService sender)
        {
            if (creator.Id == userId) return "❌ Отправить запрос самому себе невозможно.";
            if (creator.Friends != null)
            {
                var friends = FriendsHelper.GetFriends(creator.Friends);
                if (friends.Any(id => id == userId)) return "❌ Этот пользователь уже у Вас в списке друзей";
            }

            var helper = new UsersHelper();
            var user = Main.Api.Users.GetUserFromId(userId);
            bool isRequest = false;
            
            if (sender.Platform == MessengerPlatform.Vkontakte)
            {
                if (user.VkId == 0)
                    return "❌ Этот пользователь зарегестрирован через telegram. Добавление в друзья между платформами пока что недоступны.";
                isRequest = true;
                sender.Text($"🏁 Пользователь {helper.GetLink(creator)} отправил Вам запрос в друзья." +
                            $"\n ❓ Чтобы принять или отклонить запрос, откройте меню телефона в разделе запросы в друзья.", user.VkId);

            }
            else
            {
                if(user.TgId == 0) 
                    return "❌ Этот пользователь зарегистрирован через ВКонтакте. Добавление в друзья между платформами пока что недоступны.";
                isRequest = true;
                sender.Text($"🏁 Пользователь {helper.GetLink(creator)} приглашает Вас на гонку!" +
                            $"\n ❓ Чтобы принять или отклонить запрос, откройте меню телефона в разделе запросы в друзья.", user.TgId);
            }

            if (isRequest)
            {
                using (var db = new Database())
                {
                    var usr = db.Users.Single(u => u.Id == userId);
                    if (usr.FriendsRequests is null) usr.FriendsRequests = "";
                    usr.FriendsRequests += $"{creator.Id};";
                    db.SaveChanges();
                }
            }
            
          
            
            return "✔ Запрос на дружбу отправлен.";
        }
        
        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "addfriend";
        public string[] Aliases => new string[0];
    }
}