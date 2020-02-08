using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class SetNumberCarCommand:INucleusCommand
    {
        public string Command => "setcarnumber";
        public string[] Aliases => new string[0];

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var numberId = long.Parse(msg.Payload.Arguments[1]);
            var carId = long.Parse(msg.Payload.Arguments[0]);

            using (var db = new Database())
            {
                var car = db.Cars.Single(c => c.Id == carId);
                if (car.Number != 0)
                {
                    var num = db.NumbersCars.Single(n => n.Id == car.Number);
                    num.CarId = 0;
                }
                car.Number = numberId;
                var number = db.NumbersCars.Single(n => n.Id == numberId);
                if (number.CarId != 0)
                {
                    var ca = db.Cars.Single(c => c.Id == number.CarId);
                    ca.Number = 0;
                }
                number.CarId = car.Id;
                db.SaveChanges();
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text("✔ Номер успешно установлен", msg.ChatId, kb.Build());
            
        }
        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}