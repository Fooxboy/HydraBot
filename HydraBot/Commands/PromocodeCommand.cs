using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class PromocodeCommand:INucleusCommand
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
            var promocodetext = string.Empty;
            try
            {
                promocodetext = msg.Text.Split(" ")[1];

            }
            catch
            {
                sender.Text("Вы не указали промокод. Пример: промокод 1234", msg.ChatId);
                return;
            }
            
            var text = string.Empty;
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            using (var db = new Database())
            {
                var promocode = db.Promocodes.SingleOrDefault(p => p.Text == promocodetext);
                if (promocode.IsActivate)
                {
                    text = "❌ Данный промокод был уже активирован.";
                }
                else
                {
                    if (promocode is null) text = "❌ Такого промокода не существует.";
                    else
                    {
                        var usr = db.Users.Single(u => u.Id == user.Id);
                        usr.DonateMoney += promocode.DonateMoney;
                        usr.Money += promocode.Money;
                        usr.Score += promocode.Experience;

                        text = "✔ Вы активировали промокод. Вы получили:";
                        if (promocode.DonateMoney != 0) text += $"\n💰 Донат рублей: {promocode.DonateMoney}";
                        if (promocode.Money != 0) text += $"\n💵 Рублей: {promocode.DonateMoney}";
                        if (promocode.Experience != 0) text += $"\n🌟 Опыт: {promocode.DonateMoney}";

                        promocode.IsActivate = true;
                    
                        db.SaveChanges();
                    }
                }
                
            }
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "promocode";
        public string[] Aliases => new string[] {"промокод", "промо", "код"};
    }
}