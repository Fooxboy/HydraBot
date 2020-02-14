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
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            var garage = Main.Api.Garages.GetGarage(user.Id);
            var showKeyboard = true;
            long offset = 0;
            
            
            try
            {
                if (msg.Payload.Arguments?.Count >= 3)
                {
                    if (msg.Payload.Arguments[2] == "setcarnumber")
                    {
                    
                    }
                }
                else
                {
                    offset = msg.Payload.Arguments[0].ToLong();
                    try
                    {
                        var idGarage = msg.Payload.Arguments[1].ToLong();
                        if(idGarage != user.Id)
                        {
                            garage = Main.Api.Garages.GetGarage(idGarage);
                            user = Main.Api.Users.GetUserFromId(idGarage);
                            showKeyboard = false;
                        }
                    }catch { }
                }
                
            }
            catch { }

            if (msg.Text.Split(" ").Length >= 2)
            {
                try
                {
                    var id = long.Parse(msg.Text.Split(" ")[1]);
                    if (user.Access > 4)
                    {
                        garage = Main.Api.Garages.GetGarage(id);
                        user = Main.Api.Users.GetUserFromId(id);

                        showKeyboard = false;
                    }
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

            var helper = new UsersHelper();

            var text = $"🔧 Гараж пользователя: {helper.GetLink(user)}" +
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
                text += $"\n 🚘 [{car.Id}] {car.Manufacturer} {car.Model} ⚙ Двигатель:  {engineText} | {carNumber} | 💔 Здоровье: {car.Health} \n";
                if (showKeyboard) 
                {
                    kb.AddButton($"🏎 {car.Id}", "actioncar", new List<string>() { car.Id.ToString() });
                    if (i == 2)
                    {
                        kb.AddLine();
                    }
                }
            }

            if(showKeyboard) text += "❓ Для дополнительных действий выберите автомобиль на клавиатуре";

            if (cars.Count > 3 && showKeyboard) kb.AddLine();
            if (offset > 0) kb.AddButton("◀ Назад ", "garage", new List<string>() { $"{offset + 1}", garage.UserId.ToString() });
            if (cars.Count > 6) kb.AddButton("Дальше ▶", "garage", new List<string>() { $"{offset + 1}", garage.UserId.ToString() });

            if (showKeyboard)
            {
                kb.AddLine();
                kb.AddButton("⚙ Двигатели", "engines");
                kb.AddButton(ButtonsHelper.ToHomeButton());
                kb.AddButton("🗄 Номера", "numbers");
            }else
            {
                kb.AddButton(ButtonsHelper.ToHomeButton());
            }
           
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
