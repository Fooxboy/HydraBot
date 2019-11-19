using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class StoreCommand : INucleusCommand
    {
        public string Command => "store";

        public string[] Aliases => new[] { "магазин" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = "❓ Выберите раздел на клавиатуре";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🚗 Автомобили", "autostore");
            kb.AddButton("🔧 Гаражи", "garagestore");
            kb.AddButton("🏢 Бизнесы", "businessstore");
            kb.AddLine();
            kb.AddButton("♻ Разное", "otherstore");
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());

            sender.Text(text, msg.ChatId, kb.Build());
           // throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
           // throw new NotImplementedException();
        }
    }
}
