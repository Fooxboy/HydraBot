using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class BusinessCommand : INucleusCommand
    {
        public string Command => "businessstore";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = $"💵 Бизнесы." +
                $"\n⚙ [1] Шиномонтаж" +
                $"\n 🚗 [2] Автомойка" +
                $"\n🔧 [3] Автосервис" +
                $"\n ";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("⚙ [1]", "buybusiness", new List<string>() {"1"});
            kb.AddButton("🚗 [2]", "buybusiness", new List<string>() { "2" });
            kb.AddButton("🔧 [3]", "buybusiness", new List<string>() { "3" });
            kb.AddLine();
            kb.AddButton("↩ Вернуться", "store");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
