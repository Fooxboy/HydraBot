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
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("", user.Id);
            var text = $"💳 У Вас в банке хранится {user.MoneyInBank} руб." +
                $"\n 💵 У Вас наличных: {user.Money} руб." +
                 $"{(user.DonateMoney == 0 ? "" : $"\n 💰 У Вас донат рублей: { user.DonateMoney} руб.")}" +
                $"\n ❓ Выберите действие на клавиатуре.";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💸 Снять с банковского счета", "withdrawmoney", color: KeyboardButtonColor.Positive);
            kb.AddButton("💹 Положить на банковский счет", "putrawmoney", color: KeyboardButtonColor.Primary);
            kb.AddLine();
            if (user.DonateMoney > 0)
            {
                kb.AddButton("💲 Обменять донат рубли", "exchangedonate", color: KeyboardButtonColor.Positive);
                kb.AddLine();
            }

            kb.AddButton("💵 Вклад", "contribution");
            kb.AddLine();
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
