using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Interfaces;
using HydraBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Commands
{
    public class ReportCommand : INucleusCommand
    {
        private readonly IApi _api;
        public ReportCommand(IApi api)
        {
            _api = api;
        }
        public string Command => "report";

        public string[] Aliases => new[] {"репорт", "/report", "реп", "rep", "/rep"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = _api.Users.GetUser(msg);

            //проверяем: отправлял ли пользователь уже репорты
            if(ReportService.Times.Any(t=> t.Id == user.Id))
            {
                var time = ReportService.Times.Single(t => t.Id == user.Id);
                if(time.Time < 0)
                {
                    sender.Text($"❌ Вы уже отправляли репорт. Подождите {time.Time} минут.", msg.ChatId);
                    return;
                }
            }

            var result = _api.Reports.AddReport(msg.Text.Replace("репорт", "").Replace("report", ""), user.Id);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            if(result) sender.Text("✔ Ваш репор был отправлен администрации!", msg.ChatId, kb.Build());
            else sender.Text("✔ Ваше сообщение не было отправлено администрации из-за технической ошибки.", msg.ChatId, kb.Build());
            ReportService.AddToTimer(user.Id);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }
    }
}
