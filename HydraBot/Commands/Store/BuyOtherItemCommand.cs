﻿using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydraBot.Models;

namespace HydraBot.Commands.Store
{
    public class BuyOtherItemCommand : INucleusCommand
    {
        public string Command => "buyitem";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var api = Main.Api;
            var item = long.Parse(msg.Payload.Arguments[0]);
            var user = api.Users.GetUser(msg);
            var garage = api.Garages.GetGarage(user.Id);
            var text = string.Empty;
            if (item == 1)
            {
                if (garage.IsPhone) text = "❌ У Вас уже куплен телефон.";
                else
                {
                    if (user.Money < 5000) text = "❌ У Вас недостаточно наличных денег.";
                    else
                    {
                        text = "✔ Вы купили телефон";
                        api.Users.RemoveMoney(user.Id, 5000);
                        api.Garages.SetPhone(user.Id, true);
                    }
                }
            }else if (item == 2)
            {
                api.Users.RemoveMoney(user.Id, 1000);
                
                using (var db = new Database())
                {
                    var gar = db.Garages.Single(g => g.UserId == user.Id);
                    var r = new Random();
                    gar.PhoneNumber = $"8900{r.Next(100, 999)}{r.Next(1000, 9999)}";

                    text = $"✔ Вы купили сим карту. Ваш номер телефона: {gar.PhoneNumber}";
                }
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад в магазин", "otherstore");
            sender.Text(text, msg.ChatId, kb.Build());
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
