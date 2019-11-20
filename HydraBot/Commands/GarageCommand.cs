using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class GarageCommand : INucleusCommand
    {
        public string Command => "garage";

        public string[] Aliases => new[] { "гараж" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var garage = Main.Api.Garages.GetGarage(msg);

            var cars = CarsHelper.GetHelper().ConvertStringToCars(garage.Cars);

            var kb = new KeyboardBuilder(bot);

            if(garage.GarageModelId == -1)
            {
                kb.AddButton("🛒 Перейти в магазин", "store", color: KeyboardButtonColor.Positive);
                kb.AddLine();
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text($"▶ Вы еще не купили гараж! Перейдите в магазин для покупки своего первого гаража!", msg.ChatId,  kb.Build());
                return;
            }

            var text = $"🔧 Ваш гараж: {garage.Name}" +
                $"\n 🆓 Свободных парковочных мест: {garage.ParkingPlaces - cars.Count}" +
                $"\n 🚕 Ваши автомобили: \n";

            
            if (cars.Count == 0) text += "\n 🏎 У Вас нет автомобилей.";
            var counter = 0;
            foreach(var car in cars)
            {
                counter++;
                text += $"\n 🚘 [{car.Id}] {car.Manufacturer} {car.Model} | ⚡ {car.Power} л.с. | ⚖ {car.Weight} \n";
                kb.AddButton($"🏎 {car.Id}", "actioncar", new List<string>() { car.Id.ToString() });
                if(counter == 4)
                {
                    kb.AddLine();
                    counter = 0;
                }
            }

            text += "❓ Для дополнительных действий выберите автомобиль на клавиатуре";
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
