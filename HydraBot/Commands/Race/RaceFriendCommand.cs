using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Race
{
    public class RaceFriendCommand:INucleusCommand
    {
        public string Command => "racefriend";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = "🏁 Укажите id (в боте) своего друга.";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Вернуться в меню гонок", "race");
            if (user.Race != 0)
            {
                text = "❌ А ну стоять. Ты уже находишься в гонке!";
            }

            UsersCommandHelper.GetHelper().Add("racefriend", user.Id);

            sender.Text(text, msg.ChatId, kb.Build());
        }


        public static string RunFriendBattle(long creatorId, long enemyId, IMessageSenderService sender, IBot bot)
        {
            using (var db = new Database())
            {
                var creator = db.Users.Single(u => u.Id == creatorId);
                if (creator.Race != 0) return "❌ Вы уже участвуете в гонке!";
                User enemy;
                try
                { 
                    enemy = db.Users.Single(u => u.Id == enemyId);
                    if (enemy.Race != 0) return "❌ Этот пользователь уже учавствует в гонке!";
                }
                catch 
                {
                    return "❌ Не найден пользователь с таким ID";
                }

                var race = new Models.Race();
                race.Creator = creatorId;
                race.Enemy = enemyId;
                race.Id = db.Races.Count() + 1;
                race.IsRequest = true;
                
                var helper = new UsersHelper();

                if (sender.Platform == MessengerPlatform.Vkontakte)
                {
                    if (enemy.VkId == 0)
                        return "❌ Этот пользователь зарегестрирован через telegram. Гонки между платформами пока что недоступны.";
                    sender.Text($"🏁 Пользователь {helper.GetLink(creator)} приглашает Вас на гонку!" +
                                $"\n ❓ Чтобы принять или отклонить гонку, откройте меню телефона в разделе гонок.", enemy.VkId);

                }
                else
                {
                    if(enemy.TgId == 0) 
                        return "❌ Этот пользователь зарегистрирован через ВКонтакте. Гонки между платформами пока что недоступны.";
                    sender.Text($"🏁 Пользователь {helper.GetLink(creator)} приглашает Вас на гонку!" +
                                $"\n ❓ Чтобы принять или отклонить гонку, откройте меню телефона в разделе гонок.", enemy.TgId);
                }
               
                creator.Race = race.Id;
                db.Races.Add(race);
                db.SaveChanges();
                UsersCommandHelper.GetHelper().Add("", user.Id);
                return "🏁 Мы отправили пользователю запрос о гонке с Вами. ";

            }
        }


        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}