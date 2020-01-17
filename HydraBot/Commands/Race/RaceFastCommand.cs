using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Race
{
    public class RaceFastCommand: INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var garageUser = Main.Api.Garages.GetGarage(user.Id);
            if (garageUser.Fuel < 5)
            {
                var kb1 = new KeyboardBuilder(bot);
                kb1.AddButton("⛽ Заправить бак", "gasstation");
                kb1.AddLine();
                kb1.AddButton("🏎 В раздел гонок", "race");
                sender.Text("❌ У Вас кончилось топливо! Заправте бак, чтобы продолжить гонки!", msg.ChatId, kb1.Build());
                return;
            }
            sender.Text("⌛ Подождите, мы подбираем Вам противника", msg.ChatId);

            var enemys = new List<RaceFindModel>();
            long scoreUser;
            using (var db = new Database())
            {
                var userCar = db.Cars.Single(c => c.Id == garageUser.SelectCar);
                scoreUser = userCar.Power + userCar.Weight;
                foreach (var enemyGarage in db.Garages)
                {
                    if (enemyGarage.SelectCar != 0)
                    {
                        if (enemyGarage.UserId != user.Id)
                        {
                            var carEnemy = db.Cars.Single(c=> c.Id == enemyGarage.SelectCar);
                            var scoreEnemy = carEnemy.Power + carEnemy.Weight;
                            enemys.Add(new RaceFindModel() { UserId = enemyGarage.UserId, Score = scoreEnemy});
                        }
                    }
                    
                }
            }

            RaceFindModel ememyRaceModel = null;

            for (long i = 1; i < 4; i++)
            {
                for (long b = 0; b < 100; b++)
                {
                    var enemy = enemys.SingleOrDefault(e => e.Score == scoreUser + b*i);
                    ememyRaceModel = enemy;
                    if (enemy != null)
                    {
                        var garage = Main.Api.Garages.GetGarage(enemy.UserId);
                        if(garage.SelectCar != 0)  break;
                    }
                }

                if (ememyRaceModel is null)
                {
                    for (long b = 0; b < 100; b++)
                    {
                        var enemy = enemys.SingleOrDefault(e => e.Score == scoreUser - i);
                        ememyRaceModel = enemy;
                        if (enemy != null)
                        {
                            var garage = Main.Api.Garages.GetGarage(enemy.UserId);
                            if(garage.SelectCar != 0)  break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            if (ememyRaceModel is null)
            {
                ememyRaceModel = new RaceFindModel() { Score = 0, UserId = -2};
            }
            
            
            var kb = new KeyboardBuilder(bot);

            kb.AddButton("🏎 Начать гонку", "RaceStart", new List<string>() {"1", $"{ememyRaceModel.UserId}"});
            
            sender.Text("✔ Мы нашли Вам противника", msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "racefast";
        public string[] Aliases => new string[0];
    }
}