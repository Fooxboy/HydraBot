using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Bank
{
    public class WithdrawCommand : INucleusCommand
    {
        public string Command => "withdrawmoney";

        public string[] Aliases => new[] {"снять", "банк снять" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("withdrawmoney", user.Id);
            var text = "💳 Напишите сумму, которую хотите снять с банковского счета.";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("Отмена", "bank", color: KeyboardButtonColor.Negative);
            sender.Text(text, msg.ChatId, kb.Build());

        }

        public static string Withdraw(User user, long count)
        {
            if (user.MoneyInBank < count) return $"❌ У Вас недостаточно средств на счету для снятия. \n" +
                    $"💳 Баланс Вашего счета: {user.MoneyInBank}";

            if (count <= 0) return "❌ Указано неверное количество";
            var inBank =  Main.Api.Users.RemoveMoneyToBank(user.Id, count);
            var cash =  Main.Api.Users.AddMoney(user.Id, count);

            return $"✔ Вы сняли с банковского счета деньги!" +
                $"\n 💳 На счету: {inBank} руб." +
                $"\n 💵 Наличных: {cash} руб.";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
