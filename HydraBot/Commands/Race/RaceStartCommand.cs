using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HydraBot.Commands.Race
{
    public class RaceStartCommand:INucleusCommand
    {
        public string Command => "racestart";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            Models.Race race;
            User userEnemy = null;
            User userCreator = null;
            Models.Garage garageEnemy = null;
            Models.Garage garageCreator = null;
            var kb = new KeyboardBuilder(bot);
            Car carCreator;
            Car carEnemy;
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

            sender.Text($"Гонка с игроком {user.Name} на автомобиле {carEnemy.Manufacturer} {carEnemy.Model} началась! Не переходите по разделам во время гонки");


        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}