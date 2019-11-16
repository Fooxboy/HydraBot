using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class FuelCommand : INucleusCommand
    {
        public string Command => "fuel";

        public string[] Aliases => new[] { "топливо", "заправить" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);

            //todo: проверка

            var array = msg.Text.Split(" ");
            Models.Garage garageUser = null;
            try
            {
                var id = long.Parse(array[1]);
                garageUser = Main.Api.Garages.GetGarage(id);
            }catch
            {
                sender.Text("❌ Вы указали неверно Id пользователя", msg.ChatId);
                return;
            }

            long count;

            try
            {
                count = long.Parse(array[2]);
            }catch
            {
                sender.Text("❌ Вы указали неверно количество топлива", msg.ChatId);
                return;
            }

            var fuel = Main.Api.Garages.AddFuel(garageUser.UserId, count);

            sender.Text($"✔ У пользователя с ID {garageUser.UserId} теперь {fuel} топлива!", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
