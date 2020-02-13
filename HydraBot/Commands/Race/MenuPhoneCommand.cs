using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.NucleusBot;
using VkNet.Enums.SafetyEnums;


namespace HydraBot.Commands.Race
{
    public class MenuPhoneCommand : INucleusCommand
    {
        public string Command => "menuphone";

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

            var text = "📱 Меню телефона";
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🧒 Мои друзья", "friends");
            kb.AddLine();
            kb.AddButton("➕ Добавить друга", "addfriend", color: KeyboardButtonColor.Positive);
            kb.AddButton("➖ Удалить друга", "removefriend", color: KeyboardButtonColor.Negative);
            kb.AddLine();
            kb.AddButton("⚠ Запросы в друзья", "requestfriends");
            kb.AddLine();
            kb.AddButton("❌ Закрыть меню", "openphone");
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
