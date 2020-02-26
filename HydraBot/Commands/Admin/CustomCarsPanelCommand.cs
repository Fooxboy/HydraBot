using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class CustomCarsPanelCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = "Доступные запросы: ";
            var kb = new KeyboardBuilder(bot);

            using (var db = new Database())
            {
                var customCars = db.CustomCars.Where(c => c.IsModerate);
                foreach (var customCar in customCars)
                {
                    text += $"\n ID: {customCar.Id} | Name: {customCar.Name}";
                    kb.AddButton($"{customCar.Id}", "customCarsActions", new List<string>() {$"{customCar.Id}"});
                    kb.AddLine();
                }
            }

            kb.AddButton(ButtonsHelper.ToHomeButton());
            UsersCommandHelper.GetHelper().Add("customCarsAdmin", user.Id);
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "customcarspanel";
        public string[] Aliases => new string[] { "автопанель"};
    }
}