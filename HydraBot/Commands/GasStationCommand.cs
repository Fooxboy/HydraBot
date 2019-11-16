using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class GasStationCommand : INucleusCommand
    {
        public string Command => "gasstation";

        public string[] Aliases => new[] {"заправка" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {

            var user = Main.Api.Users.GetUser(msg);


            var price = 95 * 45;

            if(user.Money < price)
            {
                sender.Text($"❌ У Вас недостаточно наличных, для оплаты топлива!" +
                    $"\n 💵 Ваш баланс: {user.Money}", msg.ChatId);
                return;
            }

            Main.Api.Users.RemoveMoney(user.Id, price);

            Main.Api.Garages.AddFuel(user.Id, 95);


            sender.Text("✔ Ваш бак пополнен на максимум!", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
