using System.Linq;
using System.Threading.Tasks;
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
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("addfriend", user.Id);
            sender.Text("❓ Укажите Id (в боте) друга, мы отправим пользователю запрос.", msg.ChatId);
        }


        public static string AddFriend(User creator, long userId, IMessageSenderService sender)
        {
            var friends = FriendsHelper.GetFriends(creator.Friends);
            if (friends.Any(id => id == userId)) return "❌ Этот пользователь уже у Вас в списке друзей";

            var helper = new UsersHelper();
            var user = Main.Api.Users.GetUserFromId(userId);

            if (sender.Platform == MessengerPlatform.Vkontakte)
            {
                if (user.VkId == 0)
                    return "❌ Этот пользователь зарегестрирован через telegram. Гонки между платформами пока что недоступны.";
                sender.Text($"🏁 Пользователь {helper.GetLink(creator)} приглашает Вас на гонку!" +
                            $"\n ❓ Чтобы принять или отклонить гонку, откройте меню телефона в разделе гонок.", user.VkId);

            }
            else
            {
                if(user.TgId == 0) 
                    return "❌ Этот пользователь зарегистрирован через ВКонтакте. Гонки между платформами пока что недоступны.";
                sender.Text($"🏁 Пользователь {helper.GetLink(creator)} приглашает Вас на гонку!" +
                            $"\n ❓ Чтобы принять или отклонить гонку, откройте меню телефона в разделе гонок.", user.TgId);
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