using System.Linq;
using Fooxboy.NucleusBot;
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
            var kb = new KeyboardBuilder(bot);
            var garage = Main.Api.Garages.GetGarage(msg);
            if (garage.Engines == "")
            {
                kb.AddButton("↩ Назад в гараж", "garage");

                sender.Text("❌ У Вас нет двигателей", msg.ChatId, kb.Build());
                return;
            }
            var engines = garage.Engines.Split(";").ToList();
            using (var db = new Database())
            {
                foreach (var eng in engines)
                {
                    try
                    {
                        var engine = db.Engines.Single(e => e.Id == long.Parse(eng));
                        var carText = string.Empty;
                        if (engine.CarId != 0)
                        {
                            var car = db.Cars.Single(c => c.Id == engine.CarId);
                            carText = $"🚗 Установлен в {car.Manufacturer} {car.Model}";
                        }
                        text += $"\n ⚙ {engine.Name}| ⚡ {engine.Power} л.с| ⚖ {engine.Weight} кг. {carText}";
                    }catch {}
                   
                }
            }

            kb.AddButton("⚙ Действия над двигателями", "selectengine");
            kb.AddLine();
            kb.AddButton("↩ Назад в гараж", "garage");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
           
        }
    }
}