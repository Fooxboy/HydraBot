using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class CasinoCommand:INucleusCommand
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
            if (msg.Payload?.Arguments is null)
            {
                var text = $"⚜ Количество фишек: {user.Chips}" 
                           + "\n🃏 Выберите игру на клавиатуре";
                
                var kb = new KeyboardBuilder(bot);
               
                kb.AddButton("🎲 Рулетка", "casino", new List<string>() {"1"});
                kb.AddLine();
                kb.AddButton("⚜ Купить фишки", "buychips", color: KeyboardButtonColor.Positive);
                kb.AddButton("💰 Обменять фишки", "exchangechips", color: KeyboardButtonColor.Positive);
                kb.AddLine();
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
            }
            else
            {
                if (msg.Payload.Arguments[0] == "1")
                {
                    if (msg.Payload.Arguments.Count < 2)
                    {
                        var text = "❓ Выберите цвет, на который вы хотите сделать ставку." +
                                   "\n ❗ При выгрыше Ваша ставка увеличивается в 2 раза. При ставке на зеленое в 16 раз.";
                    
                        var kb = new KeyboardBuilder(bot);
                        kb.SetOneTime();
                        kb.AddButton("⬛ Черное 1 фишка", "casino", new List<string>(){"1", "1", "1"});
                        kb.AddButton("🟥 Красное 1 фишка", "casino", new List<string>(){"1", "2", "1"});
                        kb.AddButton("🟩 Зеленое 1 фишка", "casino", new List<string>(){"1", "3", "1"});
                        kb.AddLine();
                        kb.AddButton("⬛ Черное 3 фишки", "casino", new List<string>(){"1", "1", "3"});
                        kb.AddButton("🟥 Красное 3 фишки", "casino", new List<string>(){"1", "2", "3"});
                        kb.AddButton("🟩 Зеленое 3 фишки", "casino", new List<string>(){"1", "3", "3"});
                        kb.AddLine();
                        kb.AddButton("⬛ Черное 5 фишки", "casino", new List<string>(){"1", "1", "5"});
                        kb.AddButton("🟥 Красное 5 фишки", "casino", new List<string>(){"1", "2", "5"});
                        kb.AddButton("🟩 Зеленое 5 фишки", "casino", new List<string>() {"1", "3", "5"});
                        kb.AddLine();
                        kb.AddButton(ButtonsHelper.ToHomeButton());
                        sender.Text(text, msg.ChatId, kb.Build());
                    }
                    else
                    {
                        var color = msg.Payload.Arguments[1].ToLong();
                        var count = msg.Payload.Arguments[2].ToLong();
                        if (user.Chips < count)
                        {
                           sender.Text("❌ У Вас недостаточно фишек для ставки!", msg.ChatId);
                           return;
                        }

                        using (var db = new Database())
                        {
                            var usr = db.Users.Single(u => u.Id == user.Id);
                            usr.Chips -= count;
                            db.SaveChanges();
                        }
                        
                        var r = new Random();

                        int number = r.Next(1, 4);

                        sender.Text("♻ Вы сделали ставку. Крутим рулетку...", msg.ChatId);
                        Thread.Sleep(TimeSpan.FromSeconds(5));

                        if (number == 1 && color == 1)
                        {
                            using (var db = new Database())
                            {
                                var usr = db.Users.Single(u => u.Id == user.Id);
                                user.Chips += count * 2;
                                db.SaveChanges();
                            }
                            
                            var kb = new KeyboardBuilder(bot);
                            kb.AddButton("🎲 Назад в казино", "casino");
                            kb.AddLine();
                            kb.AddButton("♣ Назад в рулетку", "casino", new List<string>() {"1"});
                            kb.AddLine();
                            kb.AddButton(ButtonsHelper.ToHomeButton());
                            sender.Text($"🎉 Вы выграли {count *2} фишек, поставив на черное!", msg.ChatId, kb.Build());
                        }else if (number == 2 && color == 2)
                        {
                            using (var db = new Database())
                            {
                                var usr = db.Users.Single(u => u.Id == user.Id);
                                user.Chips += count * 2;
                                db.SaveChanges();
                            }
                            
                            var kb = new KeyboardBuilder(bot);
                            kb.AddButton("🎲 Назад в казино", "casino");
                            kb.AddLine();
                            kb.AddButton("♣ Назад в рулетку", "casino", new List<string>() {"1"});
                            kb.AddLine();
                            kb.AddButton(ButtonsHelper.ToHomeButton());
                            sender.Text($"🎉 Вы выграли {count *2} фишек, поставив на красное!", msg.ChatId, kb.Build());
                        }else if (number == 4)
                        {
                            var r2 = new Random();
                            var number2 = r2.Next(1, 8);

                            if (number2 == 5)
                            {
                                using (var db = new Database())
                                {
                                    var usr = db.Users.Single(u => u.Id == user.Id);
                                    user.Chips += count * 16;
                                    db.SaveChanges();
                                }
                            
                                var kb = new KeyboardBuilder(bot);
                                kb.AddButton("🎲 Назад в казино", "casino");
                                kb.AddLine();
                                kb.AddButton("♣ Назад в рулетку", "casino", new List<string>() {"1"});
                                kb.AddLine();
                                kb.AddButton(ButtonsHelper.ToHomeButton());
                                sender.Text($"🎉 Вы выграли {count *16} фишек, поставив на зеленое!", msg.ChatId, kb.Build());
                            }
                        }
                        else
                        {
                            var kb = new KeyboardBuilder(bot);
                            kb.AddButton("🎲 Назад в казино", "casino");
                            kb.AddLine();
                            kb.AddButton("♣ Назад в рулетку", "casino", new List<string>() {"1"});
                            kb.AddLine();
                            kb.AddButton(ButtonsHelper.ToHomeButton());
                            sender.Text($"😥 Вы проиграли! Может попробуйте ещё раз, точно повезёт!", msg.ChatId, kb.Build());
                        }

                    }
                    
                }
            }
           
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }

        public string Command => "casino";
        public string[] Aliases => new string[0];
    }
}