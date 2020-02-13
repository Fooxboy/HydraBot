using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands.Race
{
    public class RaceFriendStartCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.IsBanned(msg)) return;

            var enemy = msg.Payload.Arguments[0].ToLong();
            var user = Main.Api.Users.GetUser(msg);
            var text = RaceFriendCommand.RunFriendBattle(user.Id, enemy, sender, bot, msg);
            sender.Text(text, msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "raceFriendStart";
        public string[] Aliases => new string[0];
    }
}