using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands.Friends
{
    public class AcceptRequestFriendCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "acceptrequestfriend";
        public string[] Aliases => new string[0];
    }
}