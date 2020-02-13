using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Store
{
    public class InfoGarageCommand : INucleusCommand
    {
        public string Command => "infogarage";

        public string[] Aliases => new string[] { };

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
            var garage = Main.Api.Garages.GetGarage(msg);
            var id = Int64.Parse(msg.Payload.Arguments[0]);
            var helper = GarageHelper.GetHelper();
            var garageModel = helper.GetGarageModel(id);

            bool isActive = garage.Name == garageModel.Name;

            var text = $"🔧 Иформация о гараже:" +
                $"\n 🚩 Название: {garageModel.Name}" +
                $"\n 🚘 Парковочных мест: {garageModel.CountPlaces}" +
                $"\n 💵 Цена: {garageModel.Price}";

            if (isActive || garage.GarageModelId >= garageModel.Id) text += "\n ✔ У Вас куплен этот гараж";

            var kb = new KeyboardBuilder(bot);
            if (!(isActive || garage.GarageModelId >= garageModel.Id)) kb.AddButton("💵 Купить гараж", "buygarage", new List<string> { garageModel.Id.ToString() }, color: KeyboardButtonColor.Positive);

            kb.AddButton("↩ Назад к гаражам", "garagestore");

            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
