using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class HomeCommand : INucleusCommand
    {
        private readonly IApi _api;
        public HomeCommand(IApi api)
        {
            _api = api;
        }
        public string Command => "menu";

        public string[] Aliases => new[] {"меню", "/menu" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            User user = _api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("", user.Id);
            var text = "❓ Выберите раздел на клавиатуре";


            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💰 Банк", "bank");
            kb.AddButton("🏪 Магазин", "store");
            kb.AddButton("🔧 Гараж", "garage");
            kb.AddLine();

            kb.AddButton("🎈 Профиль", "profile");

            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
