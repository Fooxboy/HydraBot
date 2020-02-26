using System.Linq;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Donate
{
    public class CarDonateCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = "🏎 Укажите название автомобиля." +
                       "\n ❓ Заявка на автомобиль отправляется модераторам и они подберут для Вас мощность и вес двигателя." +
                       "\n ❓ Стоимость авто: 30 донат рублей.";
            UsersCommandHelper.GetHelper().Add("carDonate", user.Id);

        }

        public static string CreateCar(string name, User user)
        {
            if (user.DonateMoney < 30) return "❌ У Вас недостаточно донат рублей.";
            using (var db = new Database())
            {
                var usr = db.Users.Single(u => u.Id == user.Id);
                usr.DonateMoney -= 30;
                
                var car = new CustomCar();
                car.Id = db.CustomCars.Count() + 1;
                car.Name = name;
                car.IsModerate = true;
                car.UserId = user.Id;
                db.CustomCars.Add(car);
                db.SaveChanges();
            }

            return "🏎 Ваш автомобиль отправлен на модерацию, Вы получите уведомление, если Ваш авто примут.";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "carDonate";
        public string[] Aliases => new string[0];
    }
}