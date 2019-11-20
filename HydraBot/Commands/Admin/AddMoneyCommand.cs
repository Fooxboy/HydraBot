using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class AddMoneyCommand : INucleusCommand
    {
        public string Command => "addmoney";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            long userId;
            var api = Main.Api;
            try
            {
                userId = long.Parse(msg.Text.Split(" ")[1]);
            }catch
            {
                sender.Text("❌ Указан неверный Id пользователя.", msg.ChatId);
                return;
            }

            long countMoney;

            try
            {
                countMoney = long.Parse(msg.Text.Split(" ")[2]);
            }catch
            {
                sender.Text("❌ Указано неверное количество денег", msg.ChatId);
                return;
            }

            if(!api.Users.CheckUser(userId))
            {
                sender.Text("❌ Пользователя с таким  ID нет в базе данных!", msg.ChatId);
                return;
            }


            api.Users.AddDonateMoney(userId, countMoney);
            sender.Text("✔ Баланс пользователя пополнен", msg.ChatId);
            sender.Text($"💵 Ваш баланс донат рублей пополнил администратор на {countMoney} руб.", userId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
