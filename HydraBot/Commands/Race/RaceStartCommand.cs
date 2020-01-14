using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
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
            bool sendMessageToEnemy = true;
            bool isBot = false;
            if(msg.Payload.Arguments.Count == 0)
            {
                userEnemy = Main.Api.Users.GetUser(msg);
                //Пользователь принимает гонку.
                isBot = false;
                using(var db = new Database())
                {
                    try
                    {
                        race = db.Races.FirstOrDefault(r => r.Enemy == userEnemy.Id && r.IsRequest == true);
                    }catch
                    {
                        kb.AddButton(ButtonsHelper.ToHomeButton());
                        sender.Text("❌ Ваш противник уже отменил гонку!", msg.ChatId, kb.Build());
                        var a =db.Users.Single(u => u.Id == userEnemy.Id);
                        a.Race = 0;
                        db.SaveChanges();
                        return;
                    }

                    if (race is null)
                    {
                        kb.AddButton(ButtonsHelper.ToHomeButton());
                        sender.Text("❌ Мы не смогли найти эту гонку в базе данных.", msg.ChatId, kb.Build());
                        var a =db.Users.Single(u => u.Id == userEnemy.Id);
                        a.Race = 0;
                        db.SaveChanges();
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
            else
            {
                //если какой-то тип
                var typeRace = msg.Payload.Arguments[0].ToLong();
                if (typeRace == 1) //если это быстрая гонка
                {
                    var enemyId = msg.Payload.Arguments[0].ToLong();

                    race = new Models.Race();
                    userCreator = Main.Api.Users.GetUser(msg);
                    garageCreator = Main.Api.Garages.GetGarage(userCreator.Id);
                    race.Creator = userCreator.Id;
                    race.Enemy = enemyId;
                    race.IsRequest = false;
                    
                    using (var db = new Database())
                    {
                        carCreator = db.Cars.Single(c => c.Id == garageCreator.SelectCar);
                        db.Races.Add(race);
                        db.SaveChanges();
                    }
                    
                    
                    sendMessageToEnemy = false;
                    if (enemyId == -2) //генерим бота
                    {
                        isBot = true;
                        userEnemy = new User();
                        userEnemy.Id = -2;
                        userEnemy.Name = "Бот без имени";
                        
                        garageEnemy =  new Models.Garage();
                        carEnemy = new Car();
                        carEnemy.Id = -2;
                        
                        var r = new Random();

                        int a = r.Next(1, 3);

                        carEnemy.Power = a == 2 ? carCreator.Power + 1 : carCreator.Power - 1;

                        int b = r.Next(1, 3);
                    }
                    else
                    {
                        using (var db = new Database())
                        {
                            userEnemy = db.Users.Single(u => u.Id == enemyId);
                            garageEnemy = db.Garages.Single(g => g.UserId == enemyId);
                            carEnemy = db.Cars.Single(c => c.Id == garageEnemy.SelectCar);

                        }
                    }
                }
                
            }

            long enemyChatId = 0;
            long creatorChatId = 0;
            if(sender.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Vkontakte)
            {
               if(sendMessageToEnemy && !isBot) enemyChatId = userEnemy.VkId;
                creatorChatId = userCreator.VkId;
            }else
            {
                if(sendMessageToEnemy && !isBot) enemyChatId = userEnemy.TgId;
                creatorChatId = userCreator.TgId;
            }
           if(sendMessageToEnemy && !isBot) Task.Run(()=> sender.Text($"🏁 Гонка с игроком {userCreator.Name} на автомобиле {carCreator.Manufacturer} {carCreator.Model} ({carCreator.Number}) началась! Не переходите по разделам во время гонки", enemyChatId));
            Task.Run(()=> sender.Text($"🏁 Гонка с игроком {userEnemy.Name} на автомобиле {carEnemy.Manufacturer} {carEnemy.Model} ({carEnemy.Number}) началась! Не переходите по разделам во время гонки", creatorChatId));


            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                long scoreCreator = 0;
                long scoreEnemy = 0;
                if (carEnemy.Power > carCreator.Power) scoreEnemy += 5;
                else scoreCreator += 5;

                if (carEnemy.Weight > carCreator.Weight) scoreCreator += 3;
                else scoreEnemy += 3;

                Skills skillsCretor = null;
                Skills skillsEnemy = null;

                using (var db = new Database())
                {
                    try
                    {
                        skillsCretor = db.Skillses.Single(s => s.UserId == userCreator.Id);

                    }
                    catch
                    {
                        skillsCretor = new Skills();
                    }

                    try
                    {
                        if (isBot)
                        {
                            
                            //генерируем рандомный скилл
                            skillsEnemy = new Skills();
                            skillsEnemy.Driving = 0;
                        }
                        else
                        {
                            skillsEnemy = db.Skillses.Single(s => s.UserId == userEnemy.Id);
                        }
                    }
                    catch
                    {
                        if (isBot)
                        {
                            //генерируем рандомный скилл
                            skillsEnemy = new Skills();
                            skillsEnemy.Driving = 0;

                        }
                        else
                        {
                            skillsEnemy = new Skills();

                        }
                    }
                }

                //тут считаем навыки

                scoreCreator += skillsCretor.Driving;
                scoreEnemy += skillsEnemy.Driving;



                var winner = scoreEnemy > scoreCreator ? userEnemy : userCreator;

                using (var db = new Database())
                {
                    var raceLocal = db.Races.Single(r => r.Id == race.Id);
                    if (raceLocal.Winner != 0) winner = db.Users.Single(u => u.Id == raceLocal.Winner);
                    raceLocal.Winner = winner.Id;

                    if (winner.Id != -2)
                    {
                        var winnerLocal = db.Users.Single(u => u.Id == winner.Id);
                        winnerLocal.Money += 1000;
                        winnerLocal.Score += winnerLocal.Level * 50;
                    }


                    var usr1 = db.Users.Single(u => u.Id == raceLocal.Creator);
                    if (!isBot)
                    {
                        var usr2 = db.Users.Single(u => u.Id == raceLocal.Enemy);
                        usr2.Race = 0;
                    }

                    usr1.Race = 0;
                    db.SaveChanges();
                }

                if (!isBot && winner.Id == -2)
                {
                    Task.Run(() =>
                    {
                        var kb1 = new KeyboardBuilder(bot);
                        kb1.AddButton("🏁 Назад в гонки", "race");
                        sender.Text(
                            $"🎉 Поздравляю с победой! Вы получили: 💵 1.000 рублей и ⭐ {winner.Level * 50} опыта",
                            winner.Id == userEnemy.Id ? enemyChatId : creatorChatId, kb1.Build());
                    });
                }

                if (!isBot && winner.Id != -2)
                {
                    Task.Run(() =>
                    {
                        var kb2 = new KeyboardBuilder(bot);
                        kb2.AddButton("🏁 Назад в гонки", "race");
                        sender.Text($"🏁 Вы проиграли в этой гонке.", winner.Id == userEnemy.Id ? creatorChatId : enemyChatId, kb2.Build());
                    });
                }
               

            });
        }



        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}