using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Donate
{
    public class ExpDonateCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = $"⭐ Укажите, на какое количество донат рублей Вы хотите купить опыта." +
                       $"\n 💲 Ваш баланс донат рублей: {user.DonateMoney} руб." +
                       $"\n ❓ Стоимость: 1 донат рубль = 1000 опыта.";
            var kb = new KeyboardBuilder(bot);
            UsersCommandHelper.GetHelper().Add("expDonate", user.Id);
            kb.AddButton("❌ Отменить", "donate", color:  KeyboardButtonColor.Negative);
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public static string BuyExp(long count, User user)
        {
            if (count < 1) return "❌ Невозможно купить меньше, чем на 1 донат рубль.";
            if (user.DonateMoney < count) return "❌ У Вас недостаточно донат рублей.";

            var countExp = count * 1000;
            using (var db = new Database())
            {
                var usr = db.Users.Single(u => u.Id == user.Id);
                usr.DonateMoney -= count;
                usr.Score += countExp;

                db.SaveChanges();
            }

            return $"🌟 Вы успешно купили {countExp} опыта!";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "expDonate";
        public string[] Aliases => new string[0];
    }
}