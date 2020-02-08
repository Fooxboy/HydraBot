using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class SellNumberCommand:INucleusCommand
    {
        public string Command => "sellnumber";
        public string[] Aliases => new string[0];
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
            UsersCommandHelper.GetHelper().Add("sellnumber", user.Id);
            
            sender.Text("⌛ Укажите Id пользователя (в боте), которому хотите продать номер и цену." +
                        "\n ❓ Например: 123 10000", msg.ChatId);

            using (var db = new Database())
            {
                var numberSell = new SellNumber();
                numberSell.OperationId = db.SellNumbers.Count() + 1;
                numberSell.NumberId = msg.Payload.Arguments[0].ToLong();
                numberSell.OwnerId = user.Id;
                db.SellNumbers.Add(numberSell);
                db.SaveChanges();
            }
        }
        
        public static string Sell(User user, long userId, IMessageSenderService sender, long money)
        {
            if (user.Id == userId) return "❌ Вы не можете продать сами себе.";
            NumberCar number = null;
            using (var db = new Database())
            {
                if (!db.Users.Any(u => u.Id == userId))
                {
                    return "❌ Пользователь с таким Id не найден.";
                }
                var sellNumber = db.SellNumbers.Single(n => n.IsClose && n.OwnerId == user.Id);
                sellNumber.BuynerId = userId;
                number = db.NumbersCars.Single(c => c.Id == sellNumber.NumberId);
                db.SaveChanges();
            }
            var helper = new UsersHelper();
            var user2 = Main.Api.Users.GetUserFromId(userId);
            if (user2.VkId != 0)
            {
                if (sender.Platform == MessengerPlatform.Vkontakte)
                {
                    sender.Text($"❗ Вам пользователь {helper.GetLink(user2)} предлагает купить свой автомобильный номер {number.Number} за {money} рублей" +
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
    }
}