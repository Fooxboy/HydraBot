using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Commands.Race
{
    public class RaceStopCommand : INucleusCommand
    {
        public string Command => "racestop";

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
            if(user.Race <= 0)
            {
                kb.AddButton("🏁 Вернуться в раздел гонок", "race");
                sender.Text("❌ Вы не участвуете в гонке.", msg.ChatId, kb.Build());
                return;
            }

            using (var db = new Database())
            {
                var usr = db.Users.Single(u => u.Id == user.Id);
                if (usr.Race <= 0) return;

                var race = db.Races.FirstOrDefault(r => r.Id == usr.Race);
                race.Winner = race.Creator == usr.Id ? race.Creator : race.Enemy;
                race.IsRequest = false;
                usr.Race = 0;
                db.SaveChanges();
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
