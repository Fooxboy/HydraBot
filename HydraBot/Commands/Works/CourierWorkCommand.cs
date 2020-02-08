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
namespace HydraBot.Commands.Works
{
    public class CourierWorkCommand:INucleusCommand
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
             if (user.OnWork)
             {
                 sender.Text("❌ Вы уже на работе, дождитесь завершения и возвращайтесь!", msg.ChatId);
                 return;
             }
            var text = "⌛ Выберите время поездки:";
            var kb = new KeyboardBuilder(bot);

            if (!user.DriverLicense.Contains("A"))
            {
                text = "❌ Для того, чтобы работать курьером, Вам необходимы права категории A.";
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
                return;
            }
            
            if (msg.Payload.Arguments != null)
            {
                var time = msg.Payload.Arguments[0].ToLong();
                text = $"✔ Вы устроились на работу курьера. Вы освободитесь через: {time} минут";
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
                
                using (var db = new Database())
                {
                    var u = db.Users.Single(uu => uu.Id == user.Id);
                    u.OnWork = true;
                    db.SaveChanges();
                }
                Task.Run(() =>
                {
                    Thread.Sleep(TimeSpan.FromMinutes(time));
                    
                    using (var db = new Database())
                    {
                        var u = db.Users.Single(uu => uu.Id == user.Id);
                        u.OnWork = false;
                        db.SaveChanges();
                    }

                    text = $"✔ Вы закончили работу курьера.\n" +
                           $"💰 Вы заработали: {time * 1000} руб.";
                    Main.Api.Users.AddMoney(user.Id, time * 1000);
                    sender.Text(text, msg.ChatId, kb.Build());
                });
            }
            else
            {
                kb.AddButton("⌚ 10 Минут", "courierwork", new List<string>() {"10"});
                kb.AddButton("⌚ 15 Минут", "courierwork", new List<string>() {"15"});
                kb.AddLine();
                kb.AddButton("⌚ 30 Минут", "courierwork", new List<string>() {"30"});
                kb.AddButton("⌚ 1 Час", "courierwork", new List<string>() {"60"});
                kb.AddLine();
                kb.AddButton("↩ Назад к списку работы", "work");
                sender.Text(text, msg.ChatId, kb.Build());
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "courierwork";
        public string[] Aliases => new string[0];
    }
}