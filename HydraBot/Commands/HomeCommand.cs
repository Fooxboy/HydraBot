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
        public string Command => "home";

        public string[] Aliases => new[] {"домой", "дом", "главная", "/home" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            User user = _api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("", user.Id);
            var text = $"Тестирование.\n Информация о текущем пользователе: \n" +
                $"Id:{user.Id}" +
                $"\n Name: {user.Name}" +
                $"\n Забанен ли: {user.IsBanned}" +
                $"\n Время бана: {user.TimeBan} с." +
                $"\n Права доступа: {user.Access}" +
                $"\n Префикс: {user.Prefix}" +
                $"\n Уровень и опыт: {user.Level}|{user.Score}" +
                $"\n Telegram Id: {user.TgId}" +
                $"\n VKontakte Id: {user.VkId}";


            var kb = new KeyboardBuilder(bot);
            kb.AddButton("💰 Банк", "bank");
            kb.AddButton("🏪 Магазин", "store");
            kb.AddButton("🔧 Гараж", "garage");

            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //logger.Info("Команда Home загрузается...");
            //throw new NotImplementedException();
        }
    }
}
