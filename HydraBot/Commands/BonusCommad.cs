using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class BonusCommad : INucleusCommand
    {
        public string Command => "bonus";

        public string[] Aliases => new[] {"бонус" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
