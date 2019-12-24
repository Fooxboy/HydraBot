using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.NucleusBot;


namespace HydraBot.Commands.Race
{
    public class MenuPhoneCommand : INucleusCommand
    {
        public string Command => "menuphone";

        public string[] Aliases => new string[0];

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);

            var text = "📱 Меню телефона";
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🧒 Мои друзья", "friends");
            kb.AddLine();
            kb.AddButton("❌ Закрыть меню", "openphone");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
