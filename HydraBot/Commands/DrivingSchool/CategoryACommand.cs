using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.DrivingSchool
{
    public class CategoryACommand : INucleusCommand
    {
        public string Command => "catA";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
