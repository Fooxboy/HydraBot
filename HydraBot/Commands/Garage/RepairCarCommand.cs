using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Garage
{
    public class RepairCarCommand:INucleusCommand
    {
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

            var text = "❓ Вы можете выбрать место, где починить автомобиль." +
                       "\n 🔧 Починить самому - совершенно бесплатно, но занимает 15 минут." +
                       "\n 🔧 Починить  в сервисе - быстро, но прийдется заплатить 5000 рублей.";
            var kb = new KeyboardBuilder(bot);
            var carId = msg.Payload.Arguments[0].ToLong();
            
            if (msg.Payload.Arguments?.Count < 2)
            {
                kb.AddButton("🔧 Починить самому", "repaircar", new List<string>() {"1"});
                kb.AddLine();
                kb.AddButton("🔧 Починить  в сервисе", "repaircar", new List<string>(){"2"});
                kb.AddLine();
                kb.AddButton("🚌 В гараж", "garage");
                sender.Text(text, msg.ChatId, kb.Build());
            }
            else
            {
                var type = msg.Payload.Arguments[0].ToLong();
                if (type == 1)
                {
                    text = "✔ Вы начали ремонт своего авто. Это займет 15 минут.";
                    kb.AddButton("🔙 В гараж", "garage");
                    using (var db = new Database())
                    {
                        var car = db.Cars.Single(c=> c.Id == carId);
                        if (car.IsUnderRepair)
                        {
                            text = "❌ Этот автомобиль и так находится в ремонте!";
                            sender.Text(text, msg.ChatId, kb.Build());
                            return;
                        }
                        car.IsUnderRepair = true;
                        db.SaveChanges();
                    }
                    
                    sender.Text(text, msg.ChatId, kb.Build());

                    Task.Run(() =>
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(15));
                        using (var db = new Database())
                        {
                            var car = db.Cars.Single(c=> c.Id == carId);
                            car.IsUnderRepair = false;
                            car.Health = 100;
                            db.SaveChanges();
                        }

                        text = $"✔ Ваш автомобиль был отремонтирован!";
                        sender.Text(text, msg.ChatId);
                    });
                }
                else
                {
                    if (user.Money > 5000)
                    {
                        using (var db = new Database())
                        {
                            var car = db.Cars.Single(c => c.Id == carId);
                            car.Health = 100;
                            db.SaveChanges();
                        }
                        Main.Api.Users.RemoveMoney(user.Id, 5000);
                        text = "✔ Автомобиль был отремонтирован!";
                        kb.AddButton("🔙 Назад в гараж", "garage");
                        sender.Text(text, msg.ChatId, kb.Build());
                    }
                    else
                    {
                        text = "❌ У Вас недостаточно денег, для ремонта.";
                        sender.Text(text, msg.ChatId);
                    }
                }
            }
            

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "repaircar";
        public string[] Aliases => new string[0];
    }
}