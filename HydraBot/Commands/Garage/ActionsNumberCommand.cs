using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class ActionsNumberCommand:INucleusCommand
    {
        public string Command => "actionsnumber";
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
            
            var numberId = long.Parse(msg.Payload.Arguments[0]);
            var carId = long.Parse(msg.Payload.Arguments[1]);
            var kb = new KeyboardBuilder(bot);

            kb.AddButton("🚗 Установть на авто", $"{(carId != 0? "setcarnumber" : "garage")}", new List<string>() {carId.ToString(), numberId.ToString(), "setcarnumber"});
            kb.AddLine();
            kb.AddButton("💷 Продать", "sellnumber", new List<string>() {numberId.ToString()});
            sender.Text("❓ Выберите действие на клавиатуре", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}