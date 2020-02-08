using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class SellCarCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("sellcar", user.Id);
            
            sender.Text("⌛ Укажите Id пользователя (в боте), которому хотите продать авто и цену." +
                        "\n ❓ Например: 123 10000", msg.ChatId);

            using (var db = new Database())
            {
                var carSell = new SellCar();
                carSell.Id = db.SellCars.Count() + 1;
                carSell.CarId = msg.Payload.Arguments[0].ToLong();
                carSell.OwnerId = user.Id;
                db.SellCars.Add(carSell);
                db.SaveChanges();
            }
            
        }

        public static string Sell(User user, long userId, IMessageSenderService sender, long money)
        {
            Car car = null;
            using (var db = new Database())
            {
                var sellCar = db.SellCars.Single(c => c.IsClose == false && c.OwnerId == user.Id);
                sellCar.BuynerId = userId;
                car = db.Cars.Single(c => c.Id == sellCar.CarId);
                db.SaveChanges();
            }
            var helper = new UsersHelper();
            var user2 = Main.Api.Users.GetUserFromId(userId);
            if (user2.VkId != 0)
            {
                if (sender.Platform == MessengerPlatform.Vkontakte)
                {
                    sender.Text($"❗ Вам пользователь {helper.GetLink(user2)} предлагает купить свой автомобиль {car.Manufacturer} {car.Model} за {money} рублей" +
                                $"\n  ❓ Чтобы согласиться или отказать, перейдите в раздел ????", user2.VkId);
                }
                else
                {
                    return "❌ Мы не можем отправить приглашение этому пользователю";
                }
            }

            else
            {
                return "❌ Телеграм не поддерживается.";
            }

            return "✔ Мы отправили запрос о продаже авто. Вам прийдет уведомление о решении.";

        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }

        public string Command => "sellcar";
        public string[] Aliases => new string[0] {};
    }
}