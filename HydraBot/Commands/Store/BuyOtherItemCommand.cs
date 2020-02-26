using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Store
{
    public class BuyOtherItemCommand : INucleusCommand
    {
        public string Command => "buyitem";

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
                    gar.PhoneNumber = $"{r.Next(100000, 999999)}";
                    db.SaveChanges();
                    text = $"✔ Вы купили сим карту. Ваш номер телефона: {gar.PhoneNumber}";
                }
            }else if (item == 3)
            {
                text = "❓ Укажите желаемый номер телефона (6 цифр):";
                UsersCommandHelper.GetHelper().Add("customNumber", user.Id);
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад в магазин", "otherstore");
            sender.Text(text, msg.ChatId, kb.Build());
            //throw new NotImplementedException();
        }

        public static string CustomNumber(string number, User user)
        {
            if (number.Length != 6) return "❌ Длина номера телефона должна быть 6 цифр. Попробуйте еще раз!";

            using (var db = new Database())
            {
                var gar = db.Garages.Single(g => g.UserId == user.Id);
                gar.PhoneNumber = number;
                db.SaveChanges();
                return $"✔ Вы купили сим карту. Ваш номер телефона: {gar.PhoneNumber}";
            }
                
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
