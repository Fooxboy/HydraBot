using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydraBot.Models;

namespace HydraBot.Commands.Store
{
    public class BuyCarCommand : INucleusCommand
    {
        public string Command => "buycar";

        public string[] Aliases => new[] { "byyucar"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            var garage = Main.Api.Garages.GetGarage(user.Id);
            var car = CarsHelper.GetHelper().GetCarFromId(long.Parse(msg.Payload.Arguments[0]));
            var text = string.Empty;
            var kb = new KeyboardBuilder(bot);
            bool isAvalible = true;

            if(user.Money < car.Price)
            {
                text = $"❌ У Вас недостаточно наличных на покупку этого автомобиля." +
                    $"\n 💵 Ваш баланс: {user.Money}";
                isAvalible = false;
            }

            
            if((garage.ParkingPlaces - garage.Cars.Split(";").Length - 1) <= 0)
            {
                text = $"❌ У Вас недостаточно парковочных мест в гараже. Освободите место и попробуйте ещё раз!";
                kb.AddButton("🔧 Перейти в гараж", "garage");
                kb.AddLine();
                isAvalible = false;
            }

            if(isAvalible)
            {
                Main.Api.Users.RemoveMoney(user.Id, car.Price);
                using (var db = new Database())
                {
                    car.Id = db.Cars.Count() + 1;
                    
                    var engine = new Engine();
                    engine.Id = db.Engines.Count() + 1;
                    engine.Name = car.Manufacturer + " " + car.Model;
                    engine.Power = car.Power;
                    engine.Weight = car.Weight;
                    engine.CarId = car.Id;
                    db.Engines.Add(engine);
                    
                    car.Engine = engine.Id;
                    car.Health = 100;
                    db.Cars.Add(car);

                    var gar = db.Garages.Single(g => g.UserId == user.Id);
                    gar.Engines = gar.Engines + $"{engine.Id};";
                    gar.Cars = gar.Cars + $"{car.Id};";
                    db.SaveChanges();
                }
                text = $"🚗 Поздравляем с покупкой! Ваш новенький {car.Manufacturer} {car.Model} уже стоит в гараже!" +
                       $"\n ❗ Теперь укажите номер региона для автомобильного номера:";
                
                UsersCommandHelper.GetHelper().Add("buycarnumber", user.Id);
            }
            else
            {
                kb.AddButton("🚘 Перейти в автосалон", "autostore");
            }


            sender.Text(text, msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
