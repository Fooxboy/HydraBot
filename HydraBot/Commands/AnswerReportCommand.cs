using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.NucleusBot;
using VkNet.Exception;

namespace HydraBot.Commands
{
    public class AnswerReportCommand : INucleusCommand
    {
        private readonly IApi _api;
        public AnswerReportCommand(IApi api)
        {
            _api = api;
        }
        public string Command => "arep";

        public string[] Aliases => new[] { "ответитьрепорт", "ответ" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb = new KeyboardBuilder(bot);
                kb.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb.Build());
                return;
                
            }
            
            var userAnswer = _api.Users.GetUser(msg);
            if (userAnswer.Access < 4) return;

            var array = msg.Text.Split(' ');

            long id;
            try
            {
                id = long.Parse( array[1]);
            }catch
            {
                sender.Text("❌ Вы не указали Id репорта или указали его не верно", msg.ChatId);
                return;
            }

            var report = _api.Reports.GetReportFromId(id);
            var user = _api.Users.GetUserFromId(report.FromId);

            var chatId = msg.Platform == MessengerPlatform.Vkontakte ? user.VkId : user.TgId;
            var answer = msg.Text.Replace("arep", "").Replace("ответитьрепорт", "").Replace($"{report.Id}", "");
            _api.Reports.SetReportInfo(report.Id, msg.ChatId, answer);
            sender.Text($"🚩 Ответ на репорт с ID:{report.Id}:\n {answer}", chatId);
            sender.Text("✔ Ваш ответ отправлен.", msg.ChatId);
            

            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
