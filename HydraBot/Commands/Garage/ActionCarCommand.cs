using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class ActionCarCommand : INucleusCommand
    {
        public string Command => "actioncar";

        public string[] Aliases => new[] { "actioncarr" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var api = Main.Api;
            Car car = null;
            using (var db = new Database())
            {
                car = db.Cars.Single(c => c.Id == long.Parse(msg.Payload.Arguments[0]));
                
            }
            var text = $"❓ Выберите действие с автомобилем {car.Manufacturer} {car.Model}";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💵 Продать", "sellcar", new List<string>() {car.Id.ToString()});
            kb.AddLine();
            kb.AddButton("⚙ Сменить двигатель", "engines", new List<string>() {car.Id.ToString()});
            kb.AddLine();
            kb.AddButton("🗄 Сменить номер", "numbers", new List<string>() {car.Id.ToString()});
            kb.AddLine();
            var garage = api.Garages.GetGarage(msg);
            if(garage.SelectCar != car.Id) kb.AddButton("🏎 Выбрать для гонок", "selectcar", new List<string>() { car.Id.ToString() });
            kb.AddButton("↩ Назад", "garage");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
