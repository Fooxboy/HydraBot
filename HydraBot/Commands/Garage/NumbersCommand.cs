using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class NumbersCommand:INucleusCommand
    {
        public string Command => "numbers";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            long carId = 0;
            try
            {
                carId = long.Parse(msg.Payload.Arguments[0]);
            }catch { }

            var garage = Main.Api.Garages.GetGarage(msg);
            var text = "🗄 Ваши номера:";
            
            foreach (var num in garage.Numbers.Split(";"))
            {
                using (var db = new Database())
                {
                    var number = db.NumbersCars.Single(n => n.Id == long.Parse(num));
                    var inCarText = string.Empty;
                    if (number.CarId != 0)
                    {
                        var car = db.Cars.Single(c => c.Id == number.CarId);
                        inCarText = $"🚗 Установлен в {car.Manufacturer} {car.Model}";
                    }
                    else inCarText = "🚗 Не установлен на автомобиль";
                    text += $"▶ Номер {number.Number} {number.Region} {inCarText}";
                }

                text += "❓ Напишите номер, чтобы выполнить над ним действия.";
                
                sender.Text(text, msg.ChatId);
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}