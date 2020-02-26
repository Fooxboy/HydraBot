using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands
{
    public class DonateCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "donate";
        public string[] Aliases => new string[] {"донат"};
    }
}