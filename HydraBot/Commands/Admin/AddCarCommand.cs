using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fooxboy.NucleusBot;

namespace HydraBot.Commands.Admin
{
    public class AddCarCommand : INucleusCommand
    {
        public string Command => "addcar";

        public string[] Aliases => new string[] { };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var api = Main.Api;
            var user = api.Users.GetUser(msg);
            if(user.Access < 6)
            {
                sender.Text("❌ Вам недоступна эта команда.", msg.ChatId);
                return;
            }

            var array = msg.Text.Split(" ");
            long userId = 0;
            try
            {
                userId = long.Parse(array[1]);
            }catch
            {
                sender.Text("❌ Вы ввели неверный ID пользователя.", msg.ChatId);
                return;
            }

            Car car;

            try
            {
                var carId = long.Parse(array[2]);
                car = CarsHelper.GetHelper().Cars.Single(c => c.Id == carId);
            }
            catch
            {
                sender.Text("❌ Вы ввели неверный ID автомобиля.", msg.ChatId);
                return;
            }

            var garage = api.Garages.GetGarage(userId);

            if((garage.ParkingPlaces - garage.Cars.Length) == 0)
            {
                sender.Text("❌ У пользователя нет свободного места в гараже.", msg.ChatId);
                return;
            }

            var str = CarsHelper.GetHelper().AddCarToString(garage.Cars, car.Id);
            Main.Api.Garages.SetCars(user.Id, str);

            sender.Text($"✔ Вы выдали {car.Manufacturer} {car.Model} игроку с ID {userId}", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
