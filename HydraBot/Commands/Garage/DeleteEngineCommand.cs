using System;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class DeleteEngineCommand:INucleusCommand
    {
        public string Command => "deleteengine";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {

            var engineId = long.Parse(msg.Payload.Arguments[0]);
            var carId = long.Parse(msg.Payload.Arguments[1]);
            
            using (var db = new Database())
            {
                var car = db.Cars.Single(c => c.Id == carId);
                car.Engine = 0;
                var engine = db.Engines.Single(e => e.Id == engineId);
                engine.CarId = 0;
                db.SaveChanges();
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ В гараж", "garage");
            sender.Text($"✔ Вы сняли двигатель с автомобиля", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}