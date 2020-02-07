using System.Collections.Generic;
using Fooxboy.NucleusBot;
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
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("📕 Меню телефона", "menuphone");
            kb.AddLine();
            kb.AddButton("✔ Принять гонку", "raceStart", new List<string>());
            kb.AddButton("❌ Отклонить гонку", "raceundo", new List<string>());
            kb.AddLine();
            kb.AddButton("🏁 Вернуться в раздел гонок", "race");
            sender.Text("❓ Выберите действие на клавиатуре", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}