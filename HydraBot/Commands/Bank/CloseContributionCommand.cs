﻿using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Bank
{
    public class CloseContributionCommand : INucleusCommand
    {
        public string Command => "closecontribution";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад к вкладам", "contribution");
            long money = 0;
            using (var db = new Database())
            {
                var contr = db.Contributions.Single(c => c.UserId == user.Id);
                 money = contr.Money;
                 db.Contributions.Remove(contr);
                 db.SaveChanges();
            }

            Main.Api.Users.AddMoneyToBank(user.Id, money);
            sender.Text($"💵 На Ваш банковский счет зачислено {money} руб.", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}