using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HydraBot.Commands.Race
{
    public class RaceStartCommand:INucleusCommand
    {
        public string Command => "racestart";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            Models.Race race = null;
            User userEnemy = null;
            User userCreator = null;
            Models.Garage garageEnemy = null;
            Models.Garage garageCreator = null;
            var kb = new KeyboardBuilder(bot);
            Car carCreator = null;
            Car carEnemy = null;
            if(msg.Payload.Arguments.Count == 0)
            {
                userEnemy = Main.Api.Users.GetUser(msg);
                //Пользователь принимает гонку.

                using(var db = new Database())
                {
                    try
                    {
                        race = db.Races.Single(r => r.Enemy == userEnemy.Id && r.IsRequest == true);
                    }catch
                    {
                        kb.AddButton(ButtonsHelper.ToHomeButton());
                        sender.Text("❌ Ваш противник уже отменил гонку!", msg.ChatId, kb.Build());
                        return;
                    }

                    userCreator = db.Users.Single(u => u.Id == race.Creator);
                    garageCreator = db.Garages.Single(g => g.UserId == userCreator.Id);
                    carCreator = db.Cars.Single(c => c.Id == garageCreator.SelectCar);

                    race.IsRequest = false;
                    garageEnemy = db.Garages.Single(g => g.UserId == userEnemy.Id);
                    var usrE = db.Users.Single(u => u.Id == userEnemy.Id);
                    carEnemy = db.Cars.Single(c => c.Id == garageEnemy.SelectCar);
                    usrE.Race = race.Id;
                    db.SaveChanges();
                }
            }

            long enemyChatId = 0;
            long creatorChatId = 0;
            if(sender.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Vkontakte)
            {
                enemyChatId = userEnemy.VkId;
                creatorChatId = userCreator.VkId;
            }else
            {
                enemyChatId = userEnemy.TgId;
                creatorChatId = userCreator.TgId;
            }
            Task.Run(()=> sender.Text($"🏁 Гонка с игроком {userCreator.Name} на автомобиле {carCreator.Manufacturer} {carCreator.Model} ({carCreator.Number}) началась! Не переходите по разделам во время гонки", enemyChatId));
            Task.Run(()=> sender.Text($"🏁 Гонка с игроком {userEnemy.Name} на автомобиле {carEnemy.Manufacturer} {carEnemy.Model} ({carEnemy.Number}) началась! Не переходите по разделам во время гонки", creatorChatId));


            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));

                var winner = carEnemy.Power > carCreator.Power ? userEnemy : userCreator;

                using(var db = new Database())
                {
                    var raceLocal = db.Races.Single(r => r.Id == race.Id);
                    raceLocal.Winner = winner.Id;

                    var winnerLocal = db.Users.Single(u => u.Id == winner.Id);
                    winnerLocal.Money += 1000;
                    winnerLocal.Score += winnerLocal.Level * 50;

                    var usr1 = db.Users.Single(u => u.Id == raceLocal.Creator);
                    var usr2 = db.Users.Single(u => u.Id == raceLocal.Enemy);

                    usr1.Race = 0;
                    usr2.Race = 0;
                    db.SaveChanges();
                }

                var kb = new KeyboardBuilder(bot);
                kb.AddButton("🏁 Назад в гонки", "race");

                Task.Run(() => sender.Text($"🎉 Поздравляю с победой! Вы получили: 💵 1.000 рублей и ⭐ {winner.Level * 50} опыта", winner.Id == userEnemy.Id ? enemyChatId : creatorChatId, kb.Build()));
                Task.Run(() => sender.Text($"🏁 Вы проиграли в этой гонке.", winner.Id == userEnemy.Id? creatorChatId : enemyChatId, kb.Build()));

            });
        }



        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}