using System.Collections.Generic;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

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

            if(sender.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Vkontakte)
            {
                if(msg.ChatId > 2000000000)
                {
                    text = "❌ Заходить в раздел гонок можно только в личных сообщениях.";
                    kb.AddButton(ButtonsHelper.ToHomeButton());
                    sender.Text(text, msg.ChatId, kb.Build());
                    return;
                }
            }

            if (garage.IsPhone)
            {
                if (garage.SelectCar == 0)
                {
                    text = "❌ Вы не выбрали автомобиль для гонок, перейдите в гараж.";
                    kb.AddButton("🔧 В гараж", "garage");
                }else
                {
                    text = "❓ Выберите действие на клавиатуре.";
                    kb.AddButton("📱 Открыть телефон", "openphone");
                    kb.AddLine();
                    kb.AddButton("🏁 Быстрая гонка", "rrrrrrr", new List<string>() { "0" });
                    kb.AddLine();
                    kb.AddButton("🎭 Гонка с другом", "racefriend", new List<string>() { "0" });
                    kb.AddLine();
                    kb.AddButton(ButtonsHelper.ToHomeButton());
                }
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