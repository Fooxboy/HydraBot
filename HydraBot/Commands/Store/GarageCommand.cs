using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HydraBot.Commands.Store
{
    public class GarageCommand : INucleusCommand
    {
        public string Command => "garagestore";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            string text = "🔧 Магазин гаражей";
            var kb = new KeyboardBuilder(bot);
            var helper = GarageHelper.GetHelper();

            foreach (var garage in helper.Garages)
            {
                text += $"\n 🔧 [{garage.Id}] {garage.Name} | 🚘 Мест: {garage.CountPlaces} | 💵 Цена: {garage.Price}";
                kb.AddButton($"🔧 {garage.Id}", "infogarage", new List<string>() { garage.Id.ToString()});
                if (garage.Id == 3 || garage.Id == 7) kb.AddLine();
            }

            kb.AddLine();
            kb.AddButton("↩ Назад", "store");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            GarageHelper.GetHelper().InitGarages(logger);
        }
    }
}
