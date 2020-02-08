using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class NumbersCommand:INucleusCommand
    {
        public string Command => "numbers";
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
            
            long carId = 0;
            try
            {
                carId = long.Parse(msg.Payload.Arguments[0]);
            }catch { }

            var garage = Main.Api.Garages.GetGarage(msg);
            var text = "🗄 Ваши номера:";
            var kb = new KeyboardBuilder(bot);
            
            using (var db = new Database())
            {
                foreach (var num in garage.Numbers.Split(";"))
                {
                    if(num == "") break;
                    
                    var number = db.NumbersCars.Single(n => n.Id == num.ToLong());
                    var inCarText = string.Empty;
                    if (number.CarId != 0)
                    {
                        var car = db.Cars.Single(c => c.Id == number.CarId);
                        inCarText = $"🚗 Установлен в {car.Manufacturer} {car.Model}";
                    }
                    else inCarText = "🚗 Не установлен на автомобиль";
                    text += $"\n▶ Номер {number.Number} {number.Region} {inCarText}";
                    kb.AddButton($"{number.Number}", "actionsNumber", 
                        new List<string> {number.Id.ToString(), carId.ToString()});
                    kb.AddLine();
                }
            }
            
            text += "\n❓ Выберите номер на клавиатуре, чтобы выполнить над ним действие.";

            sender.Text(text, msg.ChatId, kb.Build());
        }


        public static bool GetActionsNumber(long userId, string num)
        {
            using (var db = new Database())
            {
                try
                {
                    var number = db.NumbersCars.Single(n => n.Number == num);
                    return number.Owner == userId;
                }
                catch
                {
                    return false;
                }
                
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}