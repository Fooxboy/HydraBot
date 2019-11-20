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

            var text = $"👾 Профиль игрока {user.Name}" +
                $"\n 🐾 ID: {user.Id}" +
                $"\n ▶ Префикс: {user.Prefix}" +
                $"\n 💵 Наличных: {user.Money}" +
                 $"{(user.DonateMoney == 0 ? "" : $"\n 💰 Донат рубли: { user.DonateMoney} руб.")}" +
                $"\n 💳 На банковском счету: {user.MoneyInBank}" +
                $"\n ⭐ Уровень: {user.Level} ({user.Score} из {user.Level * 150})";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());

            sender.Text(text, msg.ChatId, kb.Build());
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
