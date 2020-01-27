using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using Newtonsoft.Json;

namespace HydraBot.Commands.Works
{
    public class TruckerWorkCommand:INucleusCommand
    {
        public List<TruckerItem> Items { get; set; }
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
             var user = Main.Api.Users.GetUser(msg);
             if (user.OnWork)
             {
                 sender.Text("❌ Вы уже на работе, дождитесь завершения и возвращайтесь!", msg.ChatId);
                 return;
             }
            var text = "⌛ Доступные заказы:";
            var r = new Random();

            var index1 = r.Next(0, Items.Count);
            var index2 = r.Next(0, Items.Count);
            var index3 = r.Next(0, Items.Count);
            var index4 = r.Next(0, Items.Count);

            text += $"🚚 1. Перевоз: {Items[index1].Item} весом {Items[index1].Weight} тонн на расстояние {Items[index1].Distance} за {Items[index1].Money} руб. за {Items[index1].Time} минут.";
            text += $"🚚 2. Перевоз: {Items[index2].Item} весом {Items[index2].Weight} тонн на расстояние {Items[index2].Distance} за {Items[index2].Money} руб. за {Items[index2].Time} минут.";
            text += $"🚚 3. Перевоз: {Items[index3].Item} весом {Items[index3].Weight} тонн на расстояние {Items[index3].Distance} за {Items[index3].Money} руб. за {Items[index3].Time} минут.";
            text += $"🚚 4. Перевоз: {Items[index4].Item} весом {Items[index4].Weight} тонн на расстояние {Items[index4].Distance} за {Items[index4].Money} руб. за {Items[index4].Time} минут.";

            var kb = new KeyboardBuilder(bot);

            if (!user.DriverLicense.Contains("C"))
            {
                text = "❌ Для того, чтобы работать дальнобойщиком, Вам необходимы права категории C.";
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
                return;
            }
            
            if (msg.Payload.Arguments != null)
            {
                var time = msg.Payload.Arguments[0].ToLong();
                text = $"✔ Вы устроились на работу дальнобойщика. Вы освободитесь через: {time} минут";
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

                    text = $"✔ Вы закончили работу дальнобойщика.\n" +
                           $"💰 Вы заработали: {time * 1100} руб.";
                    Main.Api.Users.AddMoney(user.Id, time * 1100);
                    sender.Text(text, msg.ChatId, kb.Build());
                });
            }
            else
            {
                kb.AddButton("🚚 1", "truckerwork", new List<string>() {$"{Items[index1].Time}"});
                kb.AddButton("🚚 2", "truckerwork", new List<string>() {$"{Items[index2].Time}"});
                kb.AddLine();
                kb.AddButton("🚚 3", "truckerwork", new List<string>() {$"{Items[index3].Time}"});
                kb.AddButton("🚚 4", "truckerwork", new List<string>() {$"{Items[index4].Time}"});
                kb.AddLine();
                kb.AddButton("↩ Назад к списку работы", "work");
                sender.Text(text, msg.ChatId, kb.Build());
            }
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            var json = File.ReadAllText("TruckerWork.json");
            Items = JsonConvert.DeserializeObject<TruckerItems>(json).Items;
        }

        public string Command => "truckerwork";
        public string[] Aliases => new string[0];
    }
}