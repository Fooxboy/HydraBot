using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
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
            var userEnemy = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            Car carCreator;
            Car carEnemy;
            if(msg.Payload.Arguments.Count == 0)
            {
                //Пользователь принимает гонку.

                using(var db = new Database())
                {
                    try
                    {
                        race = db.Races.Single(r => r.Enemy == user.Id && r.IsRequest == true);
                    }catch
                    {
                        kb.AddButton(ButtonsHelper.ToHomeButton());
                        sender.Text("❌ Ваш противник уже отменил гонку!", msg.ChatId, kb.Build());
                        return;
                    }

                    
                    race.IsRequest = false;
                    var usr = db.Users.Single(u => u.Id == user.Id);
                    carEnemy = db.Cars.Single()
                    usr.Race = race.Id;
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