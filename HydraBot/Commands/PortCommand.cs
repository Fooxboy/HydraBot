using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class PortCommand:INucleusCommand
    {
        public List<Container> Containers { get; set; }
        private IMessageSenderService _sender;
        private long Time { get; set; }
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            _sender = sender;
            var text = $"📦 Порт с контейнерами (новые через {Time} мин.):";
            int counter = 1;
            foreach (var container in Containers)
            {
                text += $"\n 📦 Контейнер #{counter}:" +
                        $"\n 🏳 Страна: {container.Country}" +
                        $"\n ⚖ Вес: {container.Weight} " +
                        $"\n 💰 Ставка: {container.Price} руб. от {container.LastNamePrice}" +
                        $"\n";
                counter++;
            }
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton($"📦 #1 ({Containers[0].Price + 1000} руб.)", "port", new List<string>() {"0"});
            kb.AddLine();
            kb.AddButton($"📦 #2 ({Containers[1].Price + 1000} руб.)", "port", new List<string>() {"1"});
            kb.AddLine();
            kb.AddButton($"📦 #3 ({Containers[2].Price + 1000} руб.)", "port", new List<string>() {"2"});
            kb.AddLine();
            kb.AddButton("🔃 Обновить", "port");
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());

            if (msg.Payload.Arguments != null)
            {
                var number = Int32.Parse(msg.Payload.Arguments[0]);
                var container = Containers[number];

                var user = Main.Api.Users.GetUser(msg);
                if (Containers.Any(c => c.UserId == user.Id))
                {
                    sender.Text("❌ Вы можете сделать только одну ставку.", msg.ChatId);
                    return;
                    
                }

                var price = container.Price + 1000;
                if(user.Money < price) sender.Text("❌ У Вас недостаточно денег для ставки.", msg.ChatId);
                else
                {
                    container.Price = price;
                    container.LastNamePrice = user.Name;
                    container.UserId = user.Id;
                    sender.Text("✔ Вы сделали ставку", msg.ChatId);
                }
               
            }
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            Containers = new List<Container>();
            Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", UserId = 0, Price = 1000});
            Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", UserId = 0, Price = 1000});
            Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", UserId = 0, Price = 1000});
            Time = 5;
            Task.Run((() =>
            {
                while (true)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    if (Time < 1)
                    {
                        Time = 5;

                        foreach (var container in Containers)
                        {
                            var winner = container.UserId;
                            if (winner != 0)
                            {
                                using (var db = new Database())
                                {
                                    var text = string.Empty;
                                    var kb = new KeyboardBuilder(bot);
                                    var user = db.Users.Single(u => u.Id == winner);
                                    if (user.Money < container.Price)
                                    {
                                        text = "❌ У Вас недостаточно денег, для выкупа контейнера.";
                                    }
                                    user.Money -= container.Price;
                                    text = "✔ Вы открыли контейнер! Вам выпало:";
                                    text += "\n 📦 items";
                                    kb.AddButton("💰 Продать все ", "sellContainer", new List<string>(){"5000"});
                                    kb.AddLine();
                                    kb.AddButton(ButtonsHelper.ToHomeButton());
                                    
                                    //TODO: выдаем кэш.
                                    
                                    if (user.VkId != 0)
                                    {
                                        if (_sender.Platform == MessengerPlatform.Vkontakte)
                                        {
                                            _sender.Text(text, user.VkId, kb.Build());
                                        }
                                    }
                                    db.SaveChanges();
                                }
                            }
                            
                        }
                        
                        Containers.Clear();
                        Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", UserId = 0, Price = 1000});
                        Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", UserId = 0, Price = 1000});
                        Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", UserId = 0, Price = 1000});
                        
                    }
                    Time -= 1;
                }
            }));
        }

        public string Command => "port";
        public string[] Aliases => new string[] {"порт"};
    }
}