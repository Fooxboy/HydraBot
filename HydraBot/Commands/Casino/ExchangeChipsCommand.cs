using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System.Linq;

namespace HydraBot.Commands.Casino
{
    public class ExchangeChipsCommand:INucleusCommand
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
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("exchangechips", user.Id);
            var text = "⚜ Обмен фишек на рубли:" +
                       "♣ 1 фишка = 1000 рублей" +
                       "\n" +
                       "\n ❓ Укажите количество фишек, которое Вы хотите обменять.";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("❌ Назад в казино", "casino");
            sender.Text(text, msg.ChatId, kb.Build());
        }
        
        public static string ExchangeChips(User user, long count)
        {
            if (count <= 0) return "❌ Нельзя использовать отрицательные числа или ноль.";
            if (user.Chips < count) return "❌ У Вас недостаточно фишек для обмена.";

            using (var db = new Database())
            {
                var usr = db.Users.Single(u => u.Id == user.Id);
                usr.Chips -= count;
                usr.Money += count * 1000;
                db.SaveChanges();
            }

            return "✔ Вы обменяли фишки.";
        }
        

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "exchangechips";
        public string[] Aliases => new string[0];
    }
}