using System;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Store
{
    public class BuyCarNumberCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var text = "❓ Напишите номер региона, который Вы хотите иметь.";
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("buycarnumber", user.Id);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад в магазин", "otherstore");
            sender.Text(text, msg.ChatId);
        }

        public static string BuyNumber(User user, long region)
        {
            var api = Main.Api;
            if (user.Money >= 10000)
            {
                api.Users.RemoveMoney(user.Id, 10000);
                using (var db = new Database())
                {
                    var number = new NumberCar();
                    number.Id = db.NumbersCars.Count() + 1;
                    number.Owner = user.Id;
                    number.CarId = 0;
                    number.Region = region.ToString();
                    var r = new Random();
                    number.Number = r.Next(11111, 99999).ToString();
                    db.NumbersCars.Add(number);
                    var garages = db.Garages.Single(g => g.UserId == user.Id);
                    garages.Numbers += $"{number.Id};";
                    db.SaveChanges();
                    return $"✔ Вы купили номер {number.Number} {number.Region}";
                }
            }
            else return "❌ У Вас недостаточно денег.";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "buycarnumber";
        public string[] Aliases => new string[0];
    }
}