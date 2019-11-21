using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class UsersCommand : INucleusCommand
    {
        public string Command => "users";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            try
            {
                var argument = msg.Text.Split(" ")[1];
            }catch
            {
                var text = "🔧 Список пользователей:";
                using(var db = new Database())
                {
                    foreach(var user in db.Users)
                    {
                        text += $"\n 👤 ID:{user.Id} |[{user.Prefix}] {user.Name} | {user.Level} ({user.Score} из {user.Level* 150})";
                    }
                }

                sender.Text(text, msg.ChatId);
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
