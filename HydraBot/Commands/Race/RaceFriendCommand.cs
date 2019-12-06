using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands.Race
{
    public class RaceFriendCommand:INucleusCommand
    {
        public string Command => "racefriend";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = "🏁 Укажите id (в боте) своего друга.";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Вернуться в меню гонок", "race");
        }


        public static string RunFriendBattle(long creatorId, long enemyId)
        {
            
            
            return "";
        }


        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}