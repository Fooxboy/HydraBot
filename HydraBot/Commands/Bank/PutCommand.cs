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
    public class PutCommand : INucleusCommand
    {
        public string Command => "putrawmoney";

        public string[] Aliases => new[] { "положить"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("putrawmoney", user.Id);
            var text = "💳 Напишите сумму, которую хотите положить на банковский счет.";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("Отмена", "bank", color: KeyboardButtonColor.Negative);
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public static void PutMoney(User user)
        {
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
