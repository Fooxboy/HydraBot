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
    public class TaxiWorkCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            if (user.OnWork)
            {
                sender.Text("❌ Вы уже на работе, дождитесь завершения и возвращайтесь!", msg.ChatId);
                return;
            }
            var text = "⌛ Выберите понравившийся заказ:";
            var kb = new KeyboardBuilder(bot);

            if (!user.DriverLicense.Contains("B"))
            {
                text = "❌ Для того, чтобы работать таксистом, Вам необходимы права категории B.";
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
                return;
            }
            
            if (msg.Payload.Arguments != null)
            {
                var time = msg.Payload.Arguments[0].ToLong();
                text = $"✔ Вы устроились на работу таксистом. Вы освободитесь через: {time} минут.";
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

                    text = $"✔ Вы закончили работу таксистом.\n" +
                           $"💰 Вы заработали: {time * 5000} руб.";
                    Main.Api.Users.AddMoney(user.Id, time * 1000);
                    sender.Text(text, msg.ChatId, kb.Build());
                });
            }
            else
            {
                var r = new Random();
                var t = r.Next(2, 11);
                kb.AddButton($"⌚ {t * 10} км ({t} мин.)", "taxiwork", new List<string>() {$"{t}"});
                t = r.Next(2, 11);
                kb.AddButton($"⌚ {t * 10} км ({t} мин.)", "taxiwork", new List<string>() {$"{t}"});
                t = r.Next(2, 11);
                kb.AddLine();
                kb.AddButton($"⌚ {t * 10} км ({t} мин.)", "taxiwork", new List<string>() {$"{t}"});
                t = r.Next(2, 11);
                kb.AddButton($"⌚ {t * 10} км ({t} мин.)", "taxiwork", new List<string>() {$"{t}"});
                kb.AddLine();
                kb.AddButton("↩ Назад к списку работы", "work");
                sender.Text(text, msg.ChatId, kb.Build());
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "taxiwork";
        public string[] Aliases => new string[0];
    }
}