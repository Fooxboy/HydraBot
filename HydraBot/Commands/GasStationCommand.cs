using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.NucleusBot;

namespace HydraBot.Commands
{
    public class GasStationCommand : INucleusCommand
    {
        public string Command => "gasstation";

        public string[] Aliases => new[] {"заправка" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
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
