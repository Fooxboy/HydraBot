using System.Collections.Generic;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands
{
    public class DonateCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = "💎 Донат:" +
                       "\n 💸 Валюта - 1 донат рубль = 1 Российкий рубль." +
                       "\n ⭐ Опыт - 1000 опыта = 1 донат рубль." +
                       "\n 🏆 VIP - 49 донат рублей." +
                       "\n 🏎 Кастомный авто - 123 донат рублей.";
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💸 Валюта", "moneyDonate", new List<string>() {"0"});
            kb.AddLine();
            kb.AddButton("⭐ Опыт", "expDonate", new List<string>() {"0"});
            kb.AddLine();
            kb.AddButton("🏆 VIP", "vipDonate", new List<string>() {"0"});
            kb.AddLine();
            kb.AddButton("🏎 Кастомный авто", "carDonate", new List<string>() {"0"});
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "donate";
        public string[] Aliases => new string[] {"донат"};
    }
}