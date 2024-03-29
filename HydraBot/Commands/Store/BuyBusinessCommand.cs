﻿using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Store
{
    public class BuyBusinessCommand:INucleusCommand
    {
        public string Command => "buybusiness";
        public string[] Aliases => new string[] {};
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
            
            var id = long.Parse(msg.Payload.Arguments[0]);
            var text = string.Empty;
            var kb = new KeyboardBuilder(bot);
            var api = Main.Api;
            var user = api.Users.GetUser(msg);
            var price = 0;
            if (id == 1)
            {
                price = 1000000;
            }else if (id == 2)
            {
                price = 2500000;
            }else if (id == 3) price = 4000000;
            else if (id == 4) price = 50;
            else if (id == 5) price = 199;
            else if (id == 6) price = 192;
            
            
            if (user.Money < price)
            {
                text = $"❌ У Вас недостаточно наличных, чтобы купить этот бизнес." +
                       $"\n 💵 Ваш баланс: {user.Money} руб.";
            }
            else
            {
                api.Users.RemoveMoney(user.Id, price);
                using (var db = new Database())
                {
                    var us = db.Users.Single(u => u.Id == user.Id);
                    us.BusinessIds += $"{id},";
                    db.SaveChanges();
                }

                text = "✔ Вы купили бизнес!";
            }

            kb.AddButton("🛒 Назад в магазин", "store");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}