using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands
{
    public class SellContainerCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var money = msg.Payload.Arguments[0].ToLong();
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "sellContainer";
        public string[] Aliases => new string[0];
    }
}