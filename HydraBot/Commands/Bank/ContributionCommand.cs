using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Bank
{
    public class ContributionCommand:INucleusCommand
    {
        public string Command => "contribution";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            var text = "💵 Вклад";
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            using (var db = new Database())
            {
                try
                {
                    var contr = db.Contributions.Single(c => c.UserId == user.Id);
                    text += $"\n ⚡ Ваш текущий вклад:" +
                            $"\n 💲 Накопленная сумма: {contr.Money} руб." +
                            $"\n 📆 Осталось: {contr.CountDay} дн.";
                    kb.AddButton("💵 Закрыть вклад", "closecontribution");
                    kb.AddLine();
                }
                catch
                {
                    text += "\n 💲 Вы еще не открыли вклад.";
                    
                    kb.AddButton("💵 Открыть вклад", "opencontribution");
                    kb.AddLine();
                }
            }

            kb.AddButton("↩ Назад  в банк", "bank");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}