using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class SetAccessCommand : INucleusCommand
    {
        public string Command => "setaccess";

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

            long access;

            try
            {
                access = long.Parse(msg.Text.Split(" ")[2]);
            }
            catch
            {
                sender.Text("❌ Указано неверное количество денег", msg.ChatId);
                return;
            }

            var prefix = string.Empty;
            if (access == 0) prefix = "Игрок";
            else if (access == 1) prefix = "VIP";
            else if (access == 2) prefix = "Полицейский";
            else if (access == 3) prefix = "Спонсор";
            else if (access == 4) prefix = "Модератор";
            else if (access == 5) prefix = "Старший модератор";
            else if (access == 6) prefix = "Администратор";
            else prefix = "Игрок";

            api.Users.SetAccess(userId, access);
            api.Users.SetPrefix(userId, prefix);

            sender.Text($"✔ Вы установили игроку с ID {userId} права доступа {prefix}", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
