using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HydraBot.Commands.Garage
{
    public class ActionCarCommand : INucleusCommand
    {
        public string Command => "actioncar";

        public string[] Aliases => new[] { "actioncarr" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var api = Main.Api;
            var car = CarsHelper.GetHelper().GetCarFromId(long.Parse(msg.Payload.Arguments[0]));
            var text = $"❓ Выберите действие с автомобилем {car.Manufacturer} {car.Model}";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💵 Продать", "sell", new List<string>() {car.Id.ToString()});
            var garage = api.Garages.GetGarage(msg);
            if(garage.SelectCar != car.Id) kb.AddButton("🏎 Выбрать для гонок", "selectcar", new List<string>() { car.Id.ToString() });
            kb.AddButton("↩ Назад", "garage");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
