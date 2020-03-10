using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class OtherCommand : INucleusCommand
    {
        public string Command => "otherstore";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            var garage = Main.Api.Garages.GetGarage(msg);
            var text = $"🛒 Раздел другое:" +
                $"\n" +
                $"\n 📱 Телефон" +
                $"\n 💵 Цена: 2.500 руб." +
                $"\n 📟 Сим-карта" +
                $"\n 💵 Цена: 500 руб." +
                $"\n 🗄 Автомобильный номер" +
                $"\n 💵  Цена: 5.000 руб.";

            if (user.Access >= 1) text += "📟 Собственный номер телефона (Доступно только VIP)";

            var kb = new KeyboardBuilder(bot);
            if(!garage.IsPhone)
            {
                kb.AddButton("📱 Купить телефон", "buyitem", new List<string>() { "1" });
                kb.AddLine();
            }
            kb.AddButton("📟 Купить сим-карту", "buyitem", new List<string>() { "2" });
            kb.AddLine();
            kb.AddButton("🗄 Купить автомобильный номер", "buycarnumber");
            kb.AddLine();
            if (user.Access >= 1)
            {
                kb.AddButton("📟 Купить собственный номер телефона", "buyitem", new List<string>() {"3"});
                kb.AddLine();
            }
            kb.AddButton("↩ Назад в магазин", "store");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
