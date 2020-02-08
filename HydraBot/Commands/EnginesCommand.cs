using System.Collections.Generic;
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
            
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var text = "⚙ Ваши двигатели:";
            long fromCar = 0;
            try
            {
                fromCar = long.Parse(msg.Payload.Arguments[0]);
            }catch {}
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
                int counter = 0;
                foreach (var eng in engines)
                {
                    ++counter;
                    try
                    {
                        var engine = db.Engines.Single(e => e.Id == long.Parse(eng));
                        var carText = string.Empty;
                        if (engine.CarId != 0)
                        {
                            var car = db.Cars.Single(c => c.Id == engine.CarId);
                            carText = $"🚗 Установлен в {car.Manufacturer} {car.Model}";
                        }
                        else
                        {
                            carText = "🚗 Не установлен ни в какой автомобиль";
                        }
                        text += $"\n ⚙ {engine.Name}| ⚡ {engine.Power} л.с| ⚖ {engine.Weight} кг. {carText}";
                        kb.AddButton($"⚙ Двигатель {counter}", "selectengine",
                            new List<string>() {engine.Id.ToString(), fromCar.ToString()});
                        kb.AddLine();
                    }catch {}
                }
            }
            kb.AddLine();
            kb.AddButton("↩ Назад в гараж", "garage");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
           
        }
    }
}