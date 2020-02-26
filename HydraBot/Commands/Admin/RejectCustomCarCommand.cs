using System.Linq;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class RejectCustomCarCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var customCarId = msg.Payload.Arguments[0].ToLong();
            User ownerUser = null;
            using (var db = new Database())
            {
                var customCar = db.CustomCars.Single(c => c.Id == customCarId);
                customCar.IsAvaliable = false;
                ownerUser = db.Users.Single(u => u.Id == customCar.UserId);
                customCar.IsModerate = false;
                db.SaveChanges();
            }
            
            sender.Text("❌ Ваш запрос на кастомный авто отклонен", ownerUser.VkId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "rejectCustomCar";
        public string[] Aliases => new string[0];
    }
}