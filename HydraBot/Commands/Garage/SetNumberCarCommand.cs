using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands.Garage
{
    public class SetNumberCarCommand:INucleusCommand
    {
        public string Command { get; }
        public string[] Aliases { get; }
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
        }
        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}