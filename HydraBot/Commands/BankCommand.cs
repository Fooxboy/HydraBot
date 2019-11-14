using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class BankCommand : INucleusCommand
    {
        public string Command => "bank";

        public string[] Aliases => new[] {"банк" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("", user.Id);
            var text = $"💳 У Вас в банке хранится {user.MoneyInBank} руб." +
                $"\n 💵 У Вас наличных: {user.Money} руб." +
                $"\n ❓ Выберите действие на клавиатуре.";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💸 Снять", "withdrawmoney", color: KeyboardButtonColor.Positive);
            kb.AddButton("💹 Положить", "putrawmoney", color: KeyboardButtonColor.Primary);
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
