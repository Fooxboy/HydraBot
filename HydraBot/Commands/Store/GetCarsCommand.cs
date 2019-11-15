using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class GetCarsCommand : INucleusCommand
    {
        public string Command => "getcars";

        public string[] Aliases => new[] { "getcars"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if(msg.Payload != null)
            {
                if(msg.Payload.Arguments != null)
                {
                    var manufacture = msg.Payload.Arguments[0];

                }
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            CarsHelper.GetHelper().InitCars(logger);
        }
    }
}
