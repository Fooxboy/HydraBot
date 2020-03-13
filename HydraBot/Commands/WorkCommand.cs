using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;

namespace HydraBot.Commands
{
    public class WorkCommand:INucleusCommand
    {
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
            
            var text = "🏢 Выберите на клавиатуре нужную Вам работу.";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🚕 Таксист", "taxiwork");
            kb.AddButton("🚚 Дальнобойщик", "truckerwork");
            kb.AddLine();
            kb.AddButton("🚌 Водитель автобуса", "busdriverwork");
            kb.AddButton("📦 Курьер", "courierwork");
            kb.AddLine();
           // kb.AddButton("🚂 Машинист поезда", "traindriverwork");
           // kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "work";
        public string[] Aliases => new string[0];
    }
}