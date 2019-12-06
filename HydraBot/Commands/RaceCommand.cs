using System.Collections.Generic;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands
{
    public class RaceCommand:INucleusCommand
    {
        public string Command => "race";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = string.Empty;

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("📱 Открыть телефон", "openphone");
            kb.AddButton("🏁 Быстрая гонка", "racestart", new List<string>(){"0"});
            kb.AddButton("🎭 Гонка с другом", "racefriend", new List<string>(){"0"});

            sender.Text("❓ Выберите действие на клавиатуре.", msg.ChatId, kb.Build());
            //kb.AddButton("📕 Меню телефона")

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}