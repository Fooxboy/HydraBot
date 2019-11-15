using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
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

            if (garage.Cars.Count == 0) text += "\n 🏎 У Вас нет автомобилей.";
            foreach(var car in garage.Cars)
            {
                text += $"\n 🚘 {car.Manufacturer} {car.Model}" +
                    $"\n ⚡ {car.Power} л.с || ⚖ {car.Weight} кг. \n";
            }
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
