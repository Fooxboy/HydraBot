﻿using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Commands.Garage
{
    public class SelectCarCommand : INucleusCommand
    {
        public string Command => "selectcar";

        public string[] Aliases => new string[0];

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
            
            var carId = msg.Payload.Arguments[0].ToLong();
            var usr = Main.Api.Users.GetUser(msg);
            using(var db = new Database())
            {
                var garage = db.Garages.Single(g => g.UserId == usr.Id);
                garage.SelectCar = carId;
                db.SaveChanges();
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🔧 В гараж", "garage");
            sender.Text("✔ Теперь эта машина будет использоваться в гонках.", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
