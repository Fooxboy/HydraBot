using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands.Race
{
    public class OpenPhoneCommand:INucleusCommand
    {
        public string Command => "openphone";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}