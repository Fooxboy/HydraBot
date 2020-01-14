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
            sender.Text("⌛ Подождите, мы подбираем Вам противника", msg.ChatId);

            var enemys = new List<RaceFindModel>();
            long scoreUser;
            using (var db = new Database())
            {
                var userCar = db.Cars.Single(c => c.Id == garageUser.SelectCar);
                scoreUser = userCar.Power + userCar.Weight;
                foreach (var enemyGarage in db.Garages)
                {
                    if(enemyGarage.SelectCar == 0 ) break;
                    
                    var carEnemy = db.Cars.Single(c=> c.Id == enemyGarage.SelectCar);
                    var scoreEnemy = carEnemy.Power + carEnemy.Weight;
                    enemys.Add(new RaceFindModel() { UserId = enemyGarage.UserId, Score = scoreEnemy});
                }
            }

            RaceFindModel ememyRaceModel = null;

            for (long i = 1; i < 4; i++)
            {
                for (long b = 0; b < 300; b++)
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
                    for (long b = 0; b < 300; b++)
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

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "racefast";
        public string[] Aliases => new string[0];
    }
}