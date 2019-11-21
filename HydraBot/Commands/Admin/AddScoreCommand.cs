using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class AddScoreCommand : INucleusCommand
    {
        public string Command => "addscore";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            long userId;
            var api = Main.Api;
            var userSend = api.Users.GetUser(msg);
            if (userSend.Access < 6)
            {
                sender.Text("❌ Вам недоступна эта команда!", msg.ChatId);
                return;
            }

            try
            {
                userId = long.Parse(msg.Text.Split(" ")[1]);
            }
            catch
            {
                sender.Text("❌ Указан неверный Id пользователя.", msg.ChatId);
                return;
            }

            long countMoney;

            try
            {
                countMoney = long.Parse(msg.Text.Split(" ")[2]);
            }
            catch
            {
                sender.Text("❌ Указано неверное количество опыта", msg.ChatId);
                return;
            }

            if (!api.Users.CheckUser(userId))
            {
                sender.Text("❌ Пользователя с таким  ID нет в базе данных!", msg.ChatId);
                return;
            }


            api.Users.AddDonateMoney(userId, countMoney);
            sender.Text("✔ Опыт пользователя пополнен", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
