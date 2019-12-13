using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydraBot.Models;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class GarageCommand : INucleusCommand
    {
        public string Command => "garage";

        public string[] Aliases => new[] { "гараж" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var garage = Main.Api.Garages.GetGarage(msg);

            long offset = 0;
            try
            {
                offset = msg.Payload.Arguments[0].ToLong();
                try
                {
                    var idGarage = msg.Payload.Arguments[1].ToLong();
                    if(idGarage != user.Id) garage = Main.Api.Garages.GetGarage(idGarage);
                }catch { }
            }
            catch { }

            if (msg.Text.Split(" ").Length >= 2)
            {
                try
                {
                    var id = long.Parse(msg.Text.Split(" ")[1]);
                    if (user.Access > 4)
                        garage = Main.Api.Garages.GetGarage(id);
                }
                catch { }
            }

            var cars = new List<Car>();
            if (garage.Cars != "")
            {
                var carIds = garage.Cars.Split(";");
                using (var db = new Database())
                {
                    foreach (var carId in carIds)
                    {
                        try
                        {
                            var id = long.Parse(carId);
                            var carModel = db.Cars.Single(c => c.Id == id);
                            cars.Add(carModel);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
            }
           
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

            
            
            if (cars.Count == 0)
            {
                text += "\n 🏎 У Вас пока нет автомобилей.";
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
                return;
            }


            var counter = cars.Count < 6 ? cars.Count : 6;


            for (int i = Convert.ToInt32(offset) * 6; i < counter; i++)
            {
                var car = cars[i];
                var engineText = string.Empty;
                if (car.Engine != 0)
                {
                    using (var db = new Database())
                    {
                        var engine = db.Engines.Single(e => e.Id == car.Engine);
                        engineText = $"⚡ {engine.Power} л.с. | ⚖ {engine.Weight} кг.";
                    }
                }
                else
                {
                    engineText = "не установлен";
                }

                var carNumber = string.Empty;
                if (car.Number != 0)
                {
                    using (var db = new Database())
                    {
                        var num = db.NumbersCars.Single(n => n.Id == car.Number);
                        carNumber = $"🗄 Номер: {num.Number} {num.Region}";
                    }
                }
                else carNumber = $"🗄 Номер не установлен";
                text += $"\n 🚘 [{car.Id}] {car.Manufacturer} {car.Model} ⚙ Двигатель:  {engineText} | {carNumber} \n";
                kb.AddButton($"🏎 {car.Id}", "actioncar", new List<string>() { car.Id.ToString() });
                if (i == 2)
                {
                    kb.AddLine();
                }
            }


            text += "❓ Для дополнительных действий выберите автомобиль на клавиатуре";

            if (cars.Count > 3) kb.AddLine();
            if(offset > 0) kb.AddButton("◀ Назад ", "garage", new List<string>() { $"{offset + 1}" });
            if (cars.Count > 6) kb.AddButton("Дальше ▶", "garage", new List<string>() { $"{offset + 1}", garage.UserId.ToString() });

            kb.AddLine();
            kb.AddButton("⚙ Двигатели", "engines");
            kb.AddButton(ButtonsHelper.ToHomeButton());
            kb.AddButton("🗄 Номера", "numbers");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
