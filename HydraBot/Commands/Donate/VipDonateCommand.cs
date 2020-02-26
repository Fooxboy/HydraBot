using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Donate
{
    public class VipDonateCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = string.Empty;
            var kb = new KeyboardBuilder(bot);

            if (msg.Payload.Arguments[0] == "0")
            {
                 text = "👑 VIP привелегия:" +
                           "\n ❓ У вас появится возможность:" +
                           "\n ✒ Максимально символов в нике - 20." +
                           "\n 💳 Вклады в банке до 75 млн." +
                           "\n 📱 Кастомный номер телефона" +
                           "\n ⭐ Получение X2 опыта в гонках" +
                           "\n 💰 Получение Х2 приза в гонках" +
                           "\n" +
                           "\n ✔ Цена: 45 донат рублей.";
                 kb = new KeyboardBuilder(bot);
                kb.AddButton("💲 Купить", "vipDonate", new List<string>(){"1"}, color: KeyboardButtonColor.Positive);
                kb.AddLine();
            }
            else
            {

                if (user.DonateMoney < 45) text = "❌ У Вас недостаточно донат рублей для покупки.";
                else
                {
                    if (user.Access >= 1) text = "❌ У Вас уже куплен VIP или привлегия выше.";
                    else
                    {
                        using (var db = new Database())
                        {
                            var usr = db.Users.Single(u => u.Id == user.Id);
                            usr.DonateMoney -= 45;
                            usr.Access = 1;
                            usr.Prefix = "VIP";
                            db.SaveChanges();
                        }

                        text = "👑 Поздравляю с покупкой! Вы теперь VIP!";
                    }
                }
            }
            
            kb.AddButton("↩ К донатам", "donate");
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
                
            sender.Text(text, msg.ChatId, kb.Build());
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "vipDonate";
        public string[] Aliases => new string[0];
    }
}