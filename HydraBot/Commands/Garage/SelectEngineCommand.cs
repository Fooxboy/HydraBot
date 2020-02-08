using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class SelectEngineCommand :INucleusCommand
    {
        public string Command => "selectengine";
        public string[] Aliases => new string[] {};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var idEngine = long.Parse(msg.Payload.Arguments[0]);
            var idCar = long.Parse(msg.Payload.Arguments[1]);
            Engine engine = null;
            Car car = null;
            using (var db = new Database())
            {
                 engine = db.Engines.Single(e => e.Id == idEngine);
                 if (engine.CarId != 0) car = db.Cars.Single(c => c.Id == engine.CarId);
            }
            
            var text = $"⚙ Двигатель {engine.Name}" +
                       $"\n ⚡ Мощность: {engine.Power}" +
                       $"\n ⚖ Вес: {engine.Weight}" +
                       $"{(car != null? $"\n 🚗 Установлен в {car.Manufacturer} {car.Model}":string.Empty)}" +
                       $"\n ❓ Выберите действие на клавиатуре";

            var kb = new KeyboardBuilder(bot);
            if (car != null)
            {
                kb.AddButton("🚗 Снять с автомобиля", "deleteengine",
                    new List<string>() {idEngine.ToString(), car.Id.ToString()});
                kb.AddLine();
            }
            else
            {
                kb.AddButton("🚗 Установить в автомобиль", $"{(idCar !=0? "setengine":"garage" )}", new List<string>()
                {
                    idCar.ToString(), idEngine.ToString()
                });
            }

            kb.AddButton("↩ Назад к двигателям", "engines");
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}