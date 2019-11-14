using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class ReportsCommand : INucleusCommand
    {
        private readonly IApi _api;
        public ReportsCommand(IApi api)
        {
            _api = api;
        }
        public string Command => "reports";

        public string[] Aliases => new[] { "репорты", "репс" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var userAnswer = _api.Users.GetUser(msg);
            if (userAnswer.Access < 4) return;
            var text = "🚩 Неотвеченные репорты: \n";
            var reports = _api.Reports.GetReports();
            foreach(var report in reports)
            {
                text += $"▪ ID Репорта: {report.Id} \n" +
                    $"▪ Отправил: id пользователя {report.FromId}\n" +
                    $"▪ Сообщение: {report.Message} \n\n";
            }

            text += "❓ Для ответа на репорт, напишите arep <ID репорта> <Ваше сообщение> ";

            sender.Text(text, msg.ChatId);

            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
