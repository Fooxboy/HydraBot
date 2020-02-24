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

namespace HydraBot.Commands.Store
{
    public class GetCarsCommand : INucleusCommand
    {
        public string Command => "getcars";

        public string[] Aliases => new[] { "getcars"};

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
            if(msg.Payload != null)
            {
                if (msg.Payload.Arguments != null)
                {
                    var text = "🚘 Список автомобилей:\n";

                    var kb = new KeyboardBuilder(bot);
                    var manufacture = string.Empty;
                    try
                    {
                        manufacture = msg.Payload.Arguments[0];
                    }catch
                    {
                        sender.Text("❌ Эту команду можно вызвать только с аргументом через кнопку.", msg.ChatId);
                        return;
                    }

                    List<Car> cars; 
                    if (manufacture == "Rus")
                    {
                        var l1 = CarsHelper.GetHelper().Cars.Where(c => c.Manufacturer == "ВАЗ").ToList();
                        var l2 = CarsHelper.GetHelper().Cars.Where(c => c.Manufacturer == "Лада").ToList();
                        var l3 = CarsHelper.GetHelper().Cars.Where(c => c.Manufacturer == "Нива").ToList();
                        var l4 = CarsHelper.GetHelper().Cars.Where(c => c.Manufacturer == "Москвич").ToList();
                        var l5 = CarsHelper.GetHelper().Cars.Where(c => c.Manufacturer == "УАЗ").ToList();
                        cars = new List<Car>();

                        foreach (var car in l1) cars.Add(car);
                        foreach (var car in l2) cars.Add(car);
                        foreach (var car in l3) cars.Add(car);
                        foreach (var car in l4) cars.Add(car);
                        foreach (var car in l5) cars.Add(car);
                    }
                    else
                    {
                        cars = CarsHelper.GetHelper().Cars.Where(c => c.Manufacturer == manufacture).ToList();
                    }

                    try
                    {
                        var offset = Int32.Parse(msg.Payload.Arguments[1]);

                        var countCars = cars.Count; 

                        for(var i=0; i < 10; i++ )
                        {
                            try
                            {
                                var car = cars[i + (offset * 10)];
                                text += $"\n🚘 [{i + (offset * 10)}] {car.Manufacturer} {car.Model}" + $"| ⚡ {car.Power} лс., | ⚖ {car.Weight} кг." +
                                $"\n💰 Цена: {car.Price} руб.\n";
                                if(sender.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Vkontakte) kb.AddButton($"🚗 {i + (offset * 10)}", "infocar", new List<string>() { car.Id.ToString()});
                                if (sender.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Telegam) kb.AddButton($"🚗 {i + (offset * 10)} [{car.Model}]", "infocar", new List<string>() { car.Id.ToString() });

                                if ((i == 3&& (countCars >4 || countCars > 14 || countCars >24) ) || (i == 7&& (countCars > 8 || countCars > 18 || countCars > 28))) 
                                    kb.AddLine();
                            }catch
                            {
                                break;
                            }
                        }

                        kb.AddLine();
                        if(offset >0) kb.AddButton($"◀ На страницу {offset - 1}", "getcars", new List<string>() { manufacture, $"{offset - 1}" });
                        kb.AddButton("↩ Назад в автосалон", "autostore");
                        if (countCars > ((offset+1) * 10)) 
                            kb.AddButton($"На страницу {offset + 1} ▶", "getcars", new List<string>() { manufacture, $"{offset + 1}" });

                    }catch
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            try
                            {
                                var car = cars[i];
                                text += $"\n🚘 [{i}] {car.Manufacturer} {car.Model}" + $"| ⚡ {car.Power} лс., | ⚖ {car.Weight} кг." +
                                $"\n💰 Цена: {car.Price} руб.\n";
                                kb.AddButton($"🚗 {i}", "infocar", new List<string>() { car.Id.ToString() });
                                if (i == 4 || i == 8) kb.AddLine();
                            }
                            catch
                            {
                                break;
                            }
                        }
                        kb.AddLine();

                        kb.AddButton("↩ Назад в автомагазин", "autostore");
                        if (cars.Count > 10) kb.AddButton("На страницу 2 ▶", "getcars", new List<string>() { manufacture, $"1" });
                    }

                    if (cars.Count == 0) text = "⚡ Автомобили от этого производителя скоро появятся!";
                    sender.Text(text, msg.ChatId, kb.Build());
                }
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            CarsHelper.GetHelper().InitCars(logger);
        }
    }
}
