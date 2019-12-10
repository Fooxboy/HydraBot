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

            var garage = Main.Api.Garages.GetGarage(msg);

            var kb = new KeyboardBuilder(bot);

            if (garage.IsPhone)
            {
                text = "❓ Выберите действие на клавиатуре.";
                kb.AddButton("📱 Открыть телефон", "openphone");
                kb.AddButton("🏁 Быстрая гонка", "rrrrrrr", new List<string>() { "0" });
                kb.AddButton("🎭 Гонка с другом", "racefriend", new List<string>() { "0" });
            }else
            {
                text = "❌ Для участия в гонках нужен телефон. Зайдите в магазин за ним!";
                kb.AddButton("🏪 Перейти в магазин", "store");
            }
            
          
           

            sender.Text(text, msg.ChatId, kb.Build());
            

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}