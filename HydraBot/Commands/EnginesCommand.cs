using System.Linq;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class EnginesCommand :INucleusCommand
    {
        public string Command => "engines";
        public string[] Aliases => new string[] {};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = "⚙ Ваши двигатели:";
            var garage = Main.Api.Garages.GetGarage(msg);
            var engines = garage.Engines.Split(";").ToList();
            using (var db = new Database())
            {
                foreach (var eng in engines)
                {
                    var engine = db.Engines.Single(e => e.Id == long.Parse(eng));
                    text += $"\n ⚙ {engine.Name}| ⚡ {engine.Power} л.с| ⚖ {engine.Weight} кг.";
                }
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
           
        }
    }
}