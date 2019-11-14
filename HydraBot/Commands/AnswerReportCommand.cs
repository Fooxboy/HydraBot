using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class AnswerReportCommand : INucleusCommand
    {
        public string Command => "arep";

        public string[] Aliases => new[] { "ответитьрепорт", "ответ" };

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
