using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class DrivingSchoolCommand : INucleusCommand
    {
        public string Command => "drivingschool";

        public string[] Aliases => new string[] {"автошкола" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = "🚗 Автошкола." +
                "\n ❓ Выберите категорию на клавиатуре ниже";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("A (1 рубль)", "catA", new List<string> {"0", "0", "0" });
            kb.AddButton("B (1 рубль)", "catB", new List<string> {"0", "0", "0" });
            kb.AddLine();
            kb.AddButton("C (1 рубль)", "catC", new List<string> { "0", "0", "0" });
            kb.AddButton("D (1 рубль)", "catD", new List<string> { "0", "0", "0" });
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
