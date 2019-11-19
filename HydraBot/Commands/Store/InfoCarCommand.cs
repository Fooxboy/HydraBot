﻿using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Store
{
    public class InfoCarCommand : INucleusCommand
    {
        public string Command => "infocar";

        public string[] Aliases => new[] {"infocars" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            long carId;
            try
            {
                 carId = Int64.Parse(msg.Payload.Arguments[0]);
                
            }
            catch
            {
                sender.Text("❌ Эту команду можно вызывать только с клавиатуры!", msg.ChatId);
                return;
            }
            var car = CarsHelper.GetHelper().GetCarFromId(carId);
            var text = $"🚗 Информация о автомобиле:" +
                $"\n 🚘 Производитель: {car.Manufacturer}" +
                $"\n 🏎 Модель: {car.Model}" +
                $"\n ⚡ Мощность: {car.Power} л.с" +
                $"\n 🅱 Масса: {car.Weight}" +
                $"\n 💰 Цена: {car.Price}";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💵 Купить", "buycar", new List<string>() { car.Id.ToString() }, color: KeyboardButtonColor.Positive);
            kb.AddLine();
            kb.AddButton("↩ Назад", "getcars", new List<string>() { car.Manufacturer, "0" });

            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}