using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class ProfileCommand : INucleusCommand
    {
        public string Command => "profile";

        public string[] Aliases => new  [] {"/profile", "профиль", "обомне"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            if(msg.Text.Split(" ").Length >= 2)
            {
                try
                {
                    var id = long.Parse(msg.Text.Split(" ")[1]);
                    if (user.Access > 4)
                        user = Main.Api.Users.GetUserFromId(id);
                }catch { }
            }

            var gar = Main.Api.Garages.GetGarage(user.Id);
            var helper = new UsersHelper();
            var text = $"👾 Профиль игрока {helper.GetLink(user)}" +
                $"\n 🐾 ID: {user.Id}" +
                $"\n ▶ Префикс: {user.Prefix}" +
                $"\n 💵 Наличных: {user.Money}" +
                 $"{(user.DonateMoney == 0 ? "" : $"\n 💰 Донат рубли: { user.DonateMoney} руб.")}" +
                $"\n 💳 На банковском счету: {user.MoneyInBank}" +
                $"{(user.Gang!= 0? $"\n 👥 Команда: {Main.Api.Gangs.GetGang(user.Gang).Name}": "")}" +
                $"\n 📟 Ваш номер телефона: {gar.PhoneNumber}" +
                $"\n ⭐ Уровень: {user.Level} ({user.Score} из {user.Level * 150})";

            var kb = new KeyboardBuilder(bot);
            if (user.BusinessIds != "")
            {
                kb.AddButton("🏢 Мой бизнес", "mybusiness");
                kb.AddLine();
            }
            kb.AddButton(ButtonsHelper.ToHomeButton());

            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
