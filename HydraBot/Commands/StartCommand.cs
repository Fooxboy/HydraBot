using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Interfaces;
using HydraBot.Models;
using HydraBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using VkNet;
using VkNet.Exception;
using VkNet.Model;

namespace HydraBot.Commands
{
    public class StartCommand : INucleusCommand
    {
        private readonly IApi _api;
        public StartCommand(IApi api)
        {
            _api = api;
        }
        public string Command => "start";

        public string[] Aliases => new[] { "/start", "начать", "привет", "старт"};

        public void Execute(Fooxboy.NucleusBot.Models.Message msg, IMessageSenderService sender, IBot bot)
        {
            
            //проверка на регистрацию.
            if (_api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text("✔ Вы уже зарегистрированы, перейдите на главный экран!", msg.ChatId, kb2.Build());
                return;
            }

            //регистрация нового юзера.
            var user = new HydraBot.Models.User();
            
            user.Access = 0;
            user.IsBanned = false;
            user.Level = 1;
            user.Prefix = "Игрок";
            user.Score = 0;
            user.TimeBan = 0;
            user.BonusDay = 1;
            user.SubOnNews = true;
            user.Money = 100000;
            user.DriverLicense = "";
            user.IsAvailbleBonus = true;
            user.TimeBonus = 0;
            if (msg.Platform == MessengerPlatform.Vkontakte)
            {
                //устанавливаем id ВКонтакте в зависимости от того куда написал пользователь. В беседу или в лс.
                if (msg.ChatId < 2000000000) user.VkId = msg.ChatId;
                else user.VkId = msg.MessageVK.FromId.Value;

                //устанавливаем никнейм
                var vkapi = new VkApi();
                vkapi.Authorize(new ApiAuthParams()
                {
                    AccessToken = Main.Token
                });
                var userName = vkapi.Users.Get(new List<long>() { msg.MessageVK.FromId.Value })[0].FirstName;
                user.Name = userName;
            }
            //устанавливаем id Телеграмма.
            else
            {
                user.TgId = msg.MessageTG.From.Id;
                user.Name = msg.MessageTG.From.FirstName;
            }

            //добавляем пользователя в бд.
            var id = _api.Users.AddUser(user);
            user.Id = id;

            var garage = new Models.Garage() { Cars = "", PhoneNumber = null,Name = "no", IsPhone= false, Engines = "", Fuel=100, GarageModelId= -1, SelectCar = -1, ParkingPlaces = 0, UserId = id};
            Main.Api.Garages.RegisterGarage(garage);

            var skills = new Skills();
            skills.UserId = id;
            using (var db = new Database())
            {
                db.Skillses.Add(skills);
                db.SaveChanges();
            }
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text("✔ Вы успешно зарегистрировались! Перейдите на главный экран, нажав на кнопку домой.", msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }
        
    }
}
