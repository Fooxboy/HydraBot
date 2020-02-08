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
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
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
            if (ids.Any(w => w == "1"))
            {
                var count = ids.Count(w => w == "1");
                textBusiness += $"\n⚙ Шиномонтаж: Количество: {count} Доход - {100 * count} руб./час";
            }

            if (ids.Any(w => w == "2"))
            {
                var count = ids.Count(w => w == "2");
                textBusiness += $"\n🚗 Автомойка:  Количество: {count} Доход - {100 * count} руб./час";
            }

            if (ids.Any(w => w == "3"))
            {
                var count = ids.Count(w => w == "3");

                textBusiness += $"\n🔧 Автосервис: Количество: {count} Доход - {100 * count} руб./час";
            }
            sender.Text($"🏢 Ваши бизнесы:\n {textBusiness}", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}