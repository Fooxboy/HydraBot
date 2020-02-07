using System;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class UnsubNewsLetterCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            using (var db = new Database())
            {
                var us = db.Users.Single(u => u.Id == user.Id);
                us.SubOnNews = false;
                db.SaveChanges();
            }
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text("✔ Вы отписались от рассылки.", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "unsubNewsLetter";
        public string[] Aliases => new string[0];
    }
}