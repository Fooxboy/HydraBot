using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class BuyOtherItemCommand : INucleusCommand
    {
        public string Command => "buyitem";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var api = Main.Api;
            var item = long.Parse(msg.Payload.Arguments[0]);
            var user = api.Users.GetUser(msg);
            var garage = api.Garages.GetGarage(user.Id);
            var text = string.Empty;
            if (item == 1)
            {
                if (garage.IsPhone) text = "❌ У Вас уже куплен телефон.";
                else
                {
                    if (user.Money < 5000) text = "❌ У Вас недостаточно наличных денег.";
                    else
                    {
                        text = "✔ Вы купили телефон";
                        api.Users.RemoveMoney(user.Id, 5000);
                        api.Garages.SetPhone(user.Id, true);
                    }
                }


            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад", "otherstore");
            sender.Text(text, msg.ChatId, kb.Build());
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
