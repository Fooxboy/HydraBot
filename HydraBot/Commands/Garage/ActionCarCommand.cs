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
            var car = CarsHelper.GetHelper().GetCarFromId(long.Parse(msg.Payload.Arguments[0]));
            var text = $"❓ Выберите действие с автомобилем {car.Manufacturer} {car.Model}";
            var kb = new KeyboardBuilder(bot);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
