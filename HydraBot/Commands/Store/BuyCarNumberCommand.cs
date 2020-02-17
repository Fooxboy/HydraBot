using System;
using System.Collections.Generic;
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
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
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
            var regions = new List<long>() {1,2, 102,3,4,5,6,7,8,9,10,11,12, 13, 113, 14,15,16,116,716,17,18,19,20,121,22,23,93,123,193,24,84,88,124,25,125,26,126,27,28,29,30,31,32,33,34,134,35,36,136,37,38,85,138,39,91,40,41,42,142,43,44,45,46,47,147,48,49,50,90,150,190,750,51,52,152,53,54,154,55,56,156,57,58,59,81,159,60,61,161,761,62,63,163,763,64,164,65,66,96,196,67, 68,69,70,71,72,73,173,74, 174,75,80,76,77,97,99,177,197,199,777,797,799,78,98,178,198,79,82,83,86,186,87,89,92,94,95};

            var character = new List<string>() {"А", "В", "Е", "К", "М", "Н", "О", "Р", "С", "Т", "Х"};

            if (regions.All(r => r != region))
            {
                return "❌ Такого региона не существует.";
            }
            
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
                    var c1 = r.Next(0, character.Count);
                    var c2 = r.Next(0, character.Count);
                    var c3 = r.Next(0, character.Count);
                    number.Number = $"{character[c1]}{r.Next(1,999)}{character[c2]}{character[c3]}|{region}";
                    db.NumbersCars.Add(number);
                    var garages = db.Garages.Single(g => g.UserId == user.Id);
                    garages.Numbers += $"{number.Id};";
                    db.SaveChanges();
                    return $"✔ Вы купили номер: {number.Number}";
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