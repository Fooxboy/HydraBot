﻿using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands
{
    public class SellContainerCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var money = msg.Payload.Arguments[0].ToLong();
            var user = Main.Api.Users.GetUser(msg);
            Main.Api.Users.AddMoney(user.Id, money);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text($"💰 Вы продали содержимое контейнера за {money}  руб.", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "sellContainer";
        public string[] Aliases => new string[0];
    }
}