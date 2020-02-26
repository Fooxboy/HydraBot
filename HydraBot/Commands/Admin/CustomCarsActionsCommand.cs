using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class CustomCarsActionsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var customCarId = msg.Payload.Arguments[0].ToLong();

            var text = $"Информация: " +
                       $"\n ID - {customCarId}";
            using (var db = new Database())
            {
                var customCar = db.CustomCars.Single(c => c.Id == customCarId);
                text += $"\n Name - {customCar.Name}" +
                        $"\n Чтобы принять или отклонить кастомную машину, нажмите на клавиатуре кнопки.";
                
            }
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("Принять", "acceptCustomCar", new List<string>() {customCarId.ToString()});
            kb.AddLine();
            kb.AddButton("Отклонить", "rejectCustomCar", new List<string>() {customCarId.ToString()});
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "customCarsActions";
        public string[] Aliases => new string[0];
    }
}