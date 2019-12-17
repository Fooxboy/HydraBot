using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Race
{
    public class MenuPhoneCommand : INucleusCommand
    {
        public string Command => "menuphone";

        public string[] Aliases => new string[0];

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
