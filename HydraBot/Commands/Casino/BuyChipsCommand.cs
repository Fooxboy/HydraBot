using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Casino
{
    public class BuyChipsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("buychips", user.Id);
            var text = "⚜ Покупка фишек для казино:" +
                       "♣ 1 фишка = 1000 рублей" +
                       "\n" +
                       "\n ❓ Укажите количество фишек, которое Вы хотите купить.";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("❌ Назад в казино", "casino");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public static string BuyChips(User user, long count)
        {
            if (count <= 0) return "❌ Нельзя использовать отрицательные числа или ноль.";

            if (user.Money < count * 1000) return "❌ У Вас недостаточно денег для покупки.";

            using (var db = new Database())
            {
                var usr = db.Users.Single(u => u.Id == user.Id);
                usr.Chips += count;
                db.SaveChanges();
            }

            return "✔ Вы купили фишек.";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "buychips";
        public string[] Aliases => new string[0];
    }
}