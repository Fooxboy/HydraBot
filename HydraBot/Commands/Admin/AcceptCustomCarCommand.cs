using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class AcceptCustomCarCommand:INucleusCommand
    {
        public static List<TempCarAccept> TempCarAccepts { get; set; }
        
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var customCarId = msg.Payload.Arguments[0].ToLong();
            var text = "Укажите мощность и массу двигателя через пробел." +
                       "\n Пример: 234 2000";
            UsersCommandHelper.GetHelper().Add("vipDonateBuy", user.Id);
            TempCarAccepts.Add(new TempCarAccept() {UserId = user.Id, IsAccepted = false, CustomCarId = customCarId});
            sender.Text(text, msg.ChatId);
        }

        public static string SetParams(long power, long weight, User user, IMessageSenderService sender)
        {
            var acceptInfo = TempCarAccepts.FirstOrDefault(t => t.UserId == user.Id && t.IsAccepted == false);
            var customCarId = acceptInfo.CustomCarId;
            User ownerUser = null;
            using (var db = new Database())
            {
                var customCar = db.CustomCars.Single(c => c.Id == customCarId);
                customCar.Power = power;
                customCar.Weight = weight;
                customCar.IsModerate = false;
                customCar.IsAvaliable = true;
                var engine = new Engine()
                {
                    Id = db.Engines.Count() + 1,
                    CarId =  db.Cars.Count() + 1,
                    Power = power,
                    Weight = weight,
                    Name = customCar.Name
                };

                db.Engines.Add(engine);

                ownerUser = db.Users.Single(u => u.Id == customCar.UserId);
                
                var car = new Car()
                {
                    Engine = engine.Id,
                    Health = 999999,
                    Id =  engine.CarId,
                    Manufacturer = "",
                    Model = customCar.Name,
                    Power = power,
                    Price = 99929299,
                    Weight = weight
                };
                db.Cars.Add(car);

                var gar = db.Garages.Single(g => g.UserId == customCar.UserId);
                gar.Engines = gar.Engines + $"{engine.Id};";
                gar.Cars = gar.Cars + $"{car.Id};";

                db.SaveChanges();
            }
            
            sender.Text("🏎 Запрос на Вашу кастомный автомобиль был принят!", ownerUser.VkId);
            return "🏎 Автомобиль теперь находится у игрока в гараже!";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            TempCarAccepts = new List<TempCarAccept>();
        }

        public string Command => "acceptCustomCar";
        public string[] Aliases => new string[0];
    }
}