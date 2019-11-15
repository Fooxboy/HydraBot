using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class GarageCommand : INucleusCommand
    {
        public string Command => "garage";

        public string[] Aliases => new[] { "гараж" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var garage = Main.Api.Garages.GetGarage(msg);

            var text = $"🚗 Ваш гараж: {garage.Name}" +
                $"\n ▪ Парковочных мест: {garage.ParkingPlaces}" +
                $"\n 🚗 Автомобили в гараже:" +
                $"\n";

            var cars = CarsHelper.GetHelper().ConvertStringToCars(garage.Cars);
            if (cars.Count == 0) text += "\n 🏎 У Вас нет автомобилей.";
            foreach(var car in cars)
            {
                text += $"\n 🚘 {car.Manufacturer} {car.Model}" +
                    $"\n ⚡ {car.Power} л.с || ⚖ {car.Weight} кг. \n";
            }

            sender.Text(text, msg.ChatId);
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
