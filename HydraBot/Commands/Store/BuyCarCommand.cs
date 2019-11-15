using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class BuyCarCommand : INucleusCommand
    {
        public string Command => "buycar";

        public string[] Aliases => new[] { "byyucar"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
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

            if((garage.ParkingPlaces - CarsHelper.GetHelper().ConvertStringToCars(garage.Cars).Count) <= 0)
            {
                text = $"❌ У Вас недостаточно парковочных мест в гараже. Освободите место и попробуйте ещё раз!";
                kb.AddButton("🔧 Перейти в гараж", "garage");
                kb.AddLine();
                isAvalible = false;
            }

            if(isAvalible)
            {
                Main.Api.Users.RemoveMoney(user.Id, car.Price);
                var str = CarsHelper.GetHelper().AddCarToString(garage.Cars, car.Id);
                Main.Api.Garages.SetCars(user.Id, str);
                text = $"🚗 Поздравляем с покупкой! Ваш новенький {car.Manufacturer} {car.Model} уже стоит в гараже!";
                kb.AddButton("🔧 Перейти в гараж", "garage");
            }

            kb.AddButton("🚘 Перейти в автосалон", "autostore");

            sender.Text(text, msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
           // throw new NotImplementedException();
        }
    }
}
