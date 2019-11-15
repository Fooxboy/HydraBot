using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class FuelCommand : INucleusCommand
    {
        public string Command => "fuel";

        public string[] Aliases => new[] { "топливо", "заправить" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
