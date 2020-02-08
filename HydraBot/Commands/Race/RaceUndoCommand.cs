using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Commands.Race
{
    public class RaceUndoCommand : INucleusCommand
    {
        public string Command => "raceundo";

        public string[] Aliases => new string[0];

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🏁 В раздел гонок", "race");
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());


            if (user.Race <= 0)
            {
                sender.Text($"❌ У Вас нет новых запросов на гонку", msg.ChatId, kb.Build());
                return;
            }

            using (var db = new Database())
            {
                var race = db.Races.FirstOrDefault(r => r.Enemy == user.Id && r.IsRequest == true);
                race.IsRequest = false;
                race.Winner = -1;
                var helper = new UsersHelper();
                var creator = db.Users.Single(u => u.Id == race.Creator);
                long chatId = 0;

                if (sender.Platform == MessengerPlatform.Vkontakte)
                {

                    chatId = creator.VkId;
                }
                else
                {
                    chatId = creator.TgId;
                }

                creator.Race = 0;

                sender.Text($"🏁 Пользователь {helper.GetLink(creator)} отклонил ваше предложение!", chatId, kb.Build());

                db.SaveChanges();
            }

            sender.Text($"✔ Вы не приняли гонку", msg.ChatId, kb.Build());

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
