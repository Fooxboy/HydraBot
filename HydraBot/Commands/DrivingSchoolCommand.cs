using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Commands
{
    public class DrivingSchoolCommand : INucleusCommand
    {
        public string Command => "drivingschool";

        public string[] Aliases => new string[] {"автошкола" };

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
            var text = "🚗 Автошкола." +
                $"{(user.DriverLicense != "" ? $"\n📃 Ваши категории прав: {user.DriverLicense}" : "")}" +
                "\n ❓ Выберите категорию на клавиатуре ниже";

            var kb = new KeyboardBuilder(bot);
            if(!(user.DriverLicense.Split(",").Any(s=> s == "A"))) kb.AddButton("A (1 рубль)", "catA", new List<string> {"0", "0", "0" });
            if (!(user.DriverLicense.Split(",").Any(s => s == "B"))) kb.AddButton("B (1 рубль)", "catB", new List<string> {"0", "0", "0" });
            if (!(user.DriverLicense.Split(",").Any(s => s == "A" || s== "B"))) kb.AddLine();
            if (!(user.DriverLicense.Split(",").Any(s => s == "C"))) kb.AddButton("C (1 рубль)", "catC", new List<string> { "0", "0", "0" });
            if (!(user.DriverLicense.Split(",").Any(s => s == "D"))) kb.AddButton("D (1 рубль)", "catD", new List<string> { "0", "0", "0" });
            if (!(user.DriverLicense.Split(",").Any(s => s == "C" || s == "D"))) kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
