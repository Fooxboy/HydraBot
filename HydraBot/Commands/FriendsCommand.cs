using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands
{
    public class FriendsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "friends";
        public string[] Aliases => new string[0];
    }
}