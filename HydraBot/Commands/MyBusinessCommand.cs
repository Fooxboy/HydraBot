using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands
{
    public class MyBusinessCommand:INucleusCommand
    {
        public string Command => "mybusiness";
        public string[] Aliases => new string[] {"мойбизнес", "бизнесы"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            if(user.BusinessIds == "")
            {
                sender.Text($"🤨 У вас нет ни одного бизнеса.", msg.ChatId, kb.Build());
                return;
            }

            var textBusiness = "";
            var ids = user.BusinessIds.Split(",");
            if (ids.Any(w => w == "1")) textBusiness += "\n⚙ Шиномонтаж: Доход - 100 руб./час";
            if (ids.Any(w => w == "2")) textBusiness += "\n🚗 Автомойка: Доход - 100 руб./час";
            if (ids.Any(w => w == "3")) textBusiness += "\n🔧 Автосервис: Доход - 100 руб./час";
            sender.Text($"🏢 Ваши бизнесы: {user.BusinessIds}", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}