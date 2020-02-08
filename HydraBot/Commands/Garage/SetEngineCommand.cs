using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class SetEngineCommand:INucleusCommand
    {
        public string Command => "setengine";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var carId = long.Parse(msg.Payload.Arguments[0]);
            var engineId = long.Parse(msg.Payload.Arguments[1]);

            using (var db = new Database())
            {
                var car = db.Cars.Single(c => c.Id == carId);
                if (car.Engine != 0)
                {
                    var eng = db.Engines.Single(e => e.Id == car.Engine);
                    eng.CarId = 0;
                }

                car.Engine = engineId;
                
                var engine = db.Engines.Single(e => e.Id == engineId);
                car.Power = engine.Power;
                car.Weight = engine.Weight;
                engine.CarId = carId;
                db.SaveChanges();
            }
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ В гараж", "garage");
            sender.Text("✔ Вы установили двигатель в машину", msg.ChatId, kb.Build());
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}