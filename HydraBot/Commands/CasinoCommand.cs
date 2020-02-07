using System.Collections.Generic;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class CasinoCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            if (msg.Payload?.Arguments is null)
            {
                var text = $"⚜ Количество фишек: {user.Chips}" 
                           + "\n🃏 Выберите игру на клавиатуре";
                
                var kb = new KeyboardBuilder(bot);
                kb.AddButton("♣ Блэкджэк", "casino", new List<string>() {"1"});
                kb.AddLine();
                kb.AddButton("🎲 Рулетка", "casino", new List<string>() {"2"});
                kb.AddLine();
                kb.AddButton("🗃 Слоты", "casino", new List<string>() {"3"});
                kb.AddLine();
                kb.AddButton("⚜ Купить фишки", "buychips", color: KeyboardButtonColor.Positive);
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
            }
            else
            {
                var text = "🚲 Раздел в работе";
                sender.Text(text, msg.ChatId);
            }
           
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }

        public string Command => "casino";
        public string[] Aliases => new string[0];
    }
}