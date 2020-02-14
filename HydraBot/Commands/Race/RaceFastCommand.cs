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
        public List<RaceUser> UserRace { get; set; }

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
            if (UserRace.All(r => r.UserId != user.Id))
            {
                UserRace.Add(new RaceUser()
                {
                    UserId =  user.Id,
                    Users =  new List<long>()
                });
            }
            
            if (user.OnWork)
            {
                sender.Text("❌ Вы не можете идти в гонку, пока находитесь на работе, дождитесь завершения и возвращайтесь!", msg.ChatId);
                return;
            }
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
                    if (enemyGarage.SelectCar != 0)
                    {
                        if (enemyGarage.UserId != user.Id)
                        {
                            if (enemyGarage.SelectCar != -1)
                            {
                                var carEnemy = db.Cars.Single(c=> c.Id == enemyGarage.SelectCar);
                                var scoreEnemy = carEnemy.Power + carEnemy.Weight;
                                var rusr = UserRace.Single(r => r.UserId == user.Id);
                                if (rusr.Users.Any(r => r != enemyGarage.UserId))
                                {
                                    enemys.Add(new RaceFindModel() { UserId = enemyGarage.UserId, Score = scoreEnemy});

                                }
                            }
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
            kb.SetOneTime();
            kb.AddButton("🏎 Начать гонку", "RaceStart", new List<string>() {"1", $"{ememyRaceModel.UserId}"});
            
            sender.Text("✔ Мы нашли Вам противника", msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
            UserRace = new List<RaceUser>();
        }

        public string Command => "racefast";
        public string[] Aliases => new string[0];
    }
}