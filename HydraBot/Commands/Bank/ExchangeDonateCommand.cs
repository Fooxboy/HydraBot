﻿using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Bank
{
    public class ExchangeDonateCommand : INucleusCommand
    {
        public string Command => "exchangedonate";

        public string[] Aliases => new string[] { };

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
            UsersCommandHelper.GetHelper().Add("exchangedonate", user.Id);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад в банк", "bank");
            sender.Text($"💲 Напишите сумму донат-рублей, которую Вы хотите перевести в рубли." +
                $"\n 🛒 Курс обмена: 1 донат рубль = 5.000 руб." +
                $"\n 💰 Ваш баланс: {user.DonateMoney}", msg.ChatId, kb.Build());
        }

        public static string Exchange(Message msg, long count)
        {
            var user = Main.Api.Users.GetUser(msg);
            if (user.DonateMoney < count) return $"❌ На Вашем счету недостаточно донат рублей." +
                    $"\n 💰 Ваш баланс донат рублей: {user.DonateMoney}";

            if (count <= 0) return "❌ Неверное число";
            var api = Main.Api;

            var donateMoney = api.Users.RemoveDonateMoney(user.Id, count);
            var money = api.Users.AddMoney(user.Id, count * 5000);

            UsersCommandHelper.GetHelper().Add("", user.Id);
            return $"✔ Вы обменяли донат рубли!" +
                $"\n 💵 Ваш баланс: {money} руб." +
                $"\n 💰 Ваш баланс донат рублей: {donateMoney} руб.";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
