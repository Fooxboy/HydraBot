using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class OtherCommand : INucleusCommand
    {
        public string Command => "otherstore";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = $"🛒 Раздел другое:" +
                $"\n" +
                $"\n 📱 Телефон" +
                $"\n 💵 Цена: 5.000 руб.";

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("📱 Купить телефон", "buyitem", new List<string>() { "1" });
            kb.AddLine();
            kb.AddButton("↩ Назад", "store");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
