using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
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
            var user = new User();
            user.Access = 0;
            user.IsBanned = false;
            user.Level = 0;
            user.Prefix = "";
            user.Name = "Имя пользователя.";
            user.Score = 0;
            user.TimeBan = 0;
            if (msg.Platform == MessengerPlatform.Vkontakte)
            {
                //устанавливаем id ВКонтакте в зависимости от того куда написал пользователь. В беседу или в лс.
                if (msg.ChatId < 2000000000) user.VkId = msg.ChatId;
                else user.VkId = msg.MessageVK.FromId.Value;
            }
            //устанавливаем id Телеграмма.
            else user.TgId = msg.MessageTG.From.Id;

            //добавляем пользователя в бд.
            _api.Users.AddUser(user);

            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text("✔ Вы успешно зарегистрировались! Перейдите на главный экран, нажав на кнопку домой.", msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }


        //var kb1 = new KeyboardBuilder(bot);
        //kb1.AddButton("➕ Зарегистрироваться", "start");
        //        sender.Text("🛑 Вы не зарегистрировались в боте. Нажмите на кнопку ниже для регистрации.", msg.ChatId, kb1.Build());
        //        return;
    }
}
