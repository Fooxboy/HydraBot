using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Commands.Race
{
    public class RaceFriendCommand:INucleusCommand
    {
        public string Command => "racefriend";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var kb = new KeyboardBuilder(bot);

            if (sender.Platform == MessengerPlatform.Vkontakte)
            {
                if (msg.ChatId > 2000000000)
                {
                    var t = "❌ Заходить в раздел гонок можно только в личных сообщениях.";
                    
                    kb.AddButton(ButtonsHelper.ToHomeButton());
                    sender.Text(t, msg.ChatId, kb.Build());
                    return;
                }
            }

            var user = Main.Api.Users.GetUser(msg);
            
            if (user.OnWork)
            {
                sender.Text("❌ Вы не можете идти в гонку, пока находитесь на работе, дождитесь завершения и возвращайтесь!", msg.ChatId);
                return;
            }
            
            var garage = Main.Api.Garages.GetGarage(user.Id);

            if(!garage.IsPhone)
            {
                var t = "❌ Для участия в гонках необходим телефон. Загляните в магазин!";

                kb.AddButton("🏪 Перейти в магазин", "store");
                sender.Text(t, msg.ChatId, kb.Build());
                return;
            }
            var text = "🏁 Укажите id (в боте) своего друга (друг должен быть в списке Ваших друзей).";

            kb.AddButton("↩ Вернуться в меню гонок", "race");
            if (user.Race != 0)
            {
                text = "❌ А ну стоять. Ты уже находишься в гонке!";
                kb.AddLine();
                kb.AddButton("❌ Отменить гонку", "racestop");
            }else
            {
                UsersCommandHelper.GetHelper().Add("racefriend", user.Id);
            }
            sender.Text(text, msg.ChatId, kb.Build());
        }


        public static string RunFriendBattle(long creatorId, long enemyId, IMessageSenderService sender, IBot bot, Message msg)
        {
            if (creatorId == enemyId) return "❌ Участвовать в гонке с самим собой невозможно.";
            
            using (var db = new Database())
            {
                var creator = db.Users.Single(u => u.Id == creatorId);
                var friends = FriendsHelper.GetFriends(creator.Friends);
                if(!friends.Any(f=> f == enemyId)) return "❌ Этот пользователь не в списке Ваших друзей.";
                
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
                UsersCommandHelper.GetHelper().Add("", creatorId);


                Task.Run(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    var kb = new KeyboardBuilder(bot);
                    kb.AddButton("❌ Отменить гонку", "racestop");
                    sender.Text("❓ Вы можете отменить гонку, если Ваш противник не принимает гонку.", msg.ChatId, kb.Build());
                });

                return "🏁 Мы отправили пользователю запрос о гонке с Вами. ";
            }
        }


        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}