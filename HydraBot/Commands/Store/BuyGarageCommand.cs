using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class BuyGarageCommand : INucleusCommand
    {
        public string Command => "buygarage";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var helper = GarageHelper.GetHelper();
            var api = Main.Api;
            var id = long.Parse(msg.Payload.Arguments[0]);
            var garageModel = helper.GetGarageModel(id);

            var user = api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            var garage = api.Garages.GetGarage(user.Id);

            var text = string.Empty;

            if (user.Money < garageModel.Price)
            {
                text = $"❌ У Вас недостаточно наличных, чтобы купить этот гараж. \n 💵 Ваш баланс: {user.Money} рублей.";
                return;
            }

            kb.AddButton("↩ Назад", "garagestore");
            kb.AddButton("🔧 Перейти в гараж", "garage");
            api.Users.RemoveMoney(user.Id, garageModel.Price);

            api.Garages.UpgrateGarage(user.Id, garageModel.Name, garageModel.CountPlaces, garageModel.Id);

            text = "✔ Вы купили новый гараж!";
            sender.Text(text, msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
