using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Bank
{
    public class OpenContributionCommand:INucleusCommand
    {
        public string Command => "opencontribution";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("opencontribution", user.Id);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад к вкладам", "contribution");
            sender.Text(" 💸 Укажите сумму и время в днях вклада", msg.ChatId);
        }

        public static string Open(long userId, long count, long days)
        {
            if (count <= 0) return "❌ Указана неверная сумма";

            var user = Main.Api.Users.GetUserFromId(userId);

            if (user.MoneyInBank < count) return "❌ У Вас недостаточно денег в банке.";

            using (var db = new Database())
            {
                var contr = new Contribution()
                {
                    UserId = userId,
                    CountDay = days,
                    Money = count
                };
                db.Contributions.Add(contr);
                db.SaveChanges();
                return $"✔ Вы открыли вклад на сумму {count} руб., на {days} дн.";
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}