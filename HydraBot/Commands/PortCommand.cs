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
        
        private List<ItemsContainer> _items { get; set; }
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
            _items = new List<ItemsContainer>();
            _items.Add(new ItemsContainer(){ Item = "Недорогая одежда", Price = new Random().Next(1000, 3500), Weight = 12 });
            _items.Add(new ItemsContainer(){ Item = "Дорогая одежда", Price = new Random().Next(50000 , 150000), Weight = 25 });
            _items.Add(new ItemsContainer(){ Item = "Удочка", Price = new Random().Next(5000 , 10000), Weight = 2 });
            _items.Add(new ItemsContainer(){ Item = "Телевизор", Price = new Random().Next(10000 , 35000), Weight = 5 });
            _items.Add(new ItemsContainer(){ Item = "Холодильник", Price = new Random().Next(25000 , 50000), Weight = 30 });
            _items.Add(new ItemsContainer(){ Item = "Картина", Price = new Random().Next(4000 , 5000), Weight = 1 });
            _items.Add(new ItemsContainer(){ Item = "Пианино", Price = new Random().Next(50000  , 50002), Weight = 45 });
            _items.Add(new ItemsContainer(){ Item = "Оружие", Price = new Random().Next( 20000  ,  35000 ), Weight = 10 });
            _items.Add(new ItemsContainer(){ Item = "Компьютер", Price = new Random().Next( 20000  ,  40000 ), Weight = 5 });
            _items.Add(new ItemsContainer(){ Item = "Телефон", Price = new Random().Next(  15000   ,  30000 ), Weight = 1 });
            _items.Add(new ItemsContainer(){ Item = "Сейф с деньгами", Price = new Random().Next(  10000   ,  100000 ), Weight = 25 });
            _items.Add(new ItemsContainer(){ Item = "Серебро", Price = new Random().Next(   35000   ,   35002 ), Weight = 1 });
            _items.Add(new ItemsContainer(){ Item = "Золото", Price = new Random().Next( 250000, 2500000), Weight = 1 });
            _items.Add(new ItemsContainer(){ Item = "Лодка", Price = new Random().Next( 20000, 20005), Weight = 10 });
            _items.Add(new ItemsContainer(){ Item = "Двигатель", Price = new Random().Next( 70000, 200000), Weight = 200 });
            _items.Add(new ItemsContainer(){ Item = "Колесо", Price = new Random().Next( 10000, 10005), Weight = 4 });
            _items.Add(new ItemsContainer(){ Item = "Рояль", Price = new Random().Next( 700000, 1500000), Weight = 55 });
            _items.Add(new ItemsContainer(){ Item = "Чемодан", Price = 3000, Weight = 2 });
            _items.Add(new ItemsContainer(){ Item = "Шкаф", Price =  20000, Weight = 15 });
            _items.Add(new ItemsContainer(){ Item = "Коллекция монет", Price =  34000, Weight = 5 });
            _items.Add(new ItemsContainer(){ Item = "Золотые украшения", Price =  65000, Weight = 2 });
            _items.Add(new ItemsContainer(){ Item = "Металлолом", Price =  1950, Weight =  15 });

            
            Containers = new List<Container>();
            var c1 = _items[Convert.ToInt32(new Random().Next(0, _items.Count()))];
            var c2 = _items[Convert.ToInt32(new Random().Next(0, _items.Count()))];
            var c3 = _items[Convert.ToInt32(new Random().Next(0, _items.Count()))];
                
            Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", Prize = c1.Price, Price = 1000, UserId = 0, Items =  c1.Item, Weight = c3.Weight});
            Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", Prize = c2.Price,  Price = 1000, UserId = 0, Items =  c2.Item, Weight = c3.Weight});
            Containers.Add(new Container() {Country = "Россия", LastNamePrice = "Новый", Name = "без имени", Prize = c3.Price,  Price = 1000, UserId = 0, Items = c3.Item, Weight = c3.Weight} );
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
                                    text += $"\n 📦 {container.Items} " +
                                            $"\n 💸 На сумму {container.Prize}";
                                    kb.AddButton("💰 Продать все ", "sellContainer", new List<string>(){container.Prize.ToString()});
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