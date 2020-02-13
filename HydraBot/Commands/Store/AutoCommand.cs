using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Store
{
    public class AutoCommand : INucleusCommand
    {
        public string Command => "autostore";

        public string[] Aliases => new [] {"автомагазин", "автосалон"};

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
            
            var kb = new KeyboardBuilder(bot);

            if (msg.Payload != null)
            {
                if (msg.Payload.Arguments != null)
                {
                    if (msg.Payload.Arguments[0] == "1")
                    {
                        kb.AddButton("Ford", "getcars", new List<string> { "Ford", "0" });
                        kb.AddButton("GMC", "getcars", new List<string> { "GMC", "0" });
                        kb.AddButton("Honda", "getcars", new List<string> { "Honda", "0" });
                        kb.AddButton("Hummer", "getcars", new List<string> { "Hummer", "0" });
                        kb.AddLine();
                        kb.AddButton("Infiniti", "getcars", new List<string> { "Infiniti", "0" });
                        kb.AddButton("Jaguar", "getcars", new List<string> { "Jaguar", "0" });
                        kb.AddButton("Jeep", "getcars", new List<string> { "Jeep", "0" });
                        kb.AddButton("Kia", "getcars", new List<string> { "Kia", "0" });
                        kb.AddLine();
                        kb.AddButton("◀", "autostore", color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                        kb.AddButton("Страница 2 ▶", "autostore", new List<string> { "2" }, color: KeyboardButtonColor.Positive);
                    }
                    else if (msg.Payload.Arguments[0] == "2")
                    {
                        kb.AddButton("Lamborghini", "getcars", new List<string> { "Lamborghini", "0" });
                        kb.AddButton("Land Rover", "getcars", new List<string> { "Land Rover", "0" });
                        kb.AddButton("Lexus", "getcars", new List<string> { "Lexus", "0" });
                        kb.AddButton("Marussia", "getcars", new List<string> { "Marussia", "0" });
                        kb.AddLine();
                        kb.AddButton("Maserati", "getcars", new List<string> { "Maserati", "0" });
                        kb.AddButton("McLaren", "getcars", new List<string> { "McLaren", "0" });
                        kb.AddButton("Mercedes-Benz", "getcars", new List<string> { "Mercedes-Benz", "0" });
                        kb.AddButton("Mitsubishi", "getcars", new List<string> { "Mitsubishi", "0" });
                        kb.AddLine();
                        kb.AddButton("◀ Страница 1", "autostore", new List<string> { "1" }, color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                        kb.AddButton("Страница 3 ▶", "autostore", new List<string> { "3" }, color: KeyboardButtonColor.Positive);
                    }
                    else if (msg.Payload.Arguments[0] == "3")
                    {
                        kb.AddButton("Nissan", "getcars", new List<string> { "Nissan", "0" });
                        kb.AddButton("Pagani", "getcars", new List<string> { "Pagani", "0" });
                        kb.AddButton("Pontiac", "getcars", new List<string> { "Pontiac", "0" });
                        kb.AddButton("Porsche", "getcars", new List<string> { "Porsche", "0" });
                        kb.AddLine();

                        kb.AddButton("Renault", "getcars", new List<string> { "Renault", "0" });
                        kb.AddButton("Rolls-Royce", "getcars", new List<string> { "Rolls-Royce", "0" });
                        kb.AddButton("Saab", "getcars", new List<string> { "Saab", "0" });
                        kb.AddButton("Skoda", "getcars", new List<string> { "Skoda", "0" });
                        kb.AddLine();
                        kb.AddButton("◀ Страница 2", "autostore", new List<string> { "2" }, color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                        kb.AddButton("Страница 4 ▶", "autostore", new List<string> { "4" }, color: KeyboardButtonColor.Positive);
                    }
                    else if (msg.Payload.Arguments[0] == "4")
                    {
                        kb.AddButton("Subaru", "getcars", new List<string> { "Subaru", "0" });
                        kb.AddButton("Tesla", "getcars", new List<string> { "Tesla", "0" });
                        kb.AddButton("Toyota", "getcars", new List<string> { "Toyota", "0" });
                        kb.AddButton("Volkswagen", "getcars", new List<string> { "Volkswagen", "0" });
                        kb.AddLine();
                        kb.AddButton("Volvo", "getcars", new List<string> { "Volvo", "0" });
                        kb.AddButton("Русский автопром", "getcars", new List<string> { "Rus", "0" });
                        kb.AddLine();
                        kb.AddButton("◀ Страница 2", "autostore", new List<string> { "2" }, color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                    }
                }
                else
                {
                    kb.AddButton("Aston Martin", "getcars", new List<string> { "Aston Martin", "0" });
                    kb.AddButton("Audi", "getcars", new List<string> { "Audi", "0" });
                    kb.AddButton("BMW", "getcars", new List<string> { "BMW", "0" });
                    kb.AddButton("Bugatti", "getcars", new List<string> { "Bugatti", "0" });
                    kb.AddLine();
                    kb.AddButton("Cadillac", "getcars", new List<string> { "Cadillac", "0" });
                    kb.AddButton("Chevrolet", "getcars", new List<string> { "Chevrolet", "0" });
                    kb.AddButton("Dodge", "getcars", new List<string> { "Dodge", "0" });
                    kb.AddButton("Ferrari", "getcars", new List<string> { "Ferrari", "0" });
                    kb.AddLine();
                    kb.AddButton("🏪 Магазин", "store");
                    kb.AddButton("Страница 1 ▶", "autostore", new List<string> { "1" }, color: KeyboardButtonColor.Positive);

                }
            }else
            {
                kb.AddButton("Aston Martin", "getcars", new List<string> { "Aston Martin", "0" });
                kb.AddButton("Audi", "getcars", new List<string> { "Audi", "0" });
                kb.AddButton("BMW", "getcars", new List<string> { "BMW", "0" });
                kb.AddButton("Bugatti", "getcars", new List<string> { "Bugatti", "0" });
                kb.AddLine();
                kb.AddButton("Cadillac", "getcars", new List<string> { "Cadillac", "0" });
                kb.AddButton("Chevrolet", "getcars", new List<string> { "Chevrolet", "0" });
                kb.AddButton("Dodge", "getcars", new List<string> { "Dodge", "0" });
                kb.AddButton("Ferrari", "getcars", new List<string> { "Ferrari", "0" });
                kb.AddLine();
                kb.AddButton("🏪 Магазин", "store");
                kb.AddButton("Страница 1 ▶", "autostore", new List<string> { "1" }, color: KeyboardButtonColor.Positive);
            }
            sender.Text("🏎 Выберите производителя автомобиля на клавиатуре.", msg.ChatId, kb.Build());
        }
        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
