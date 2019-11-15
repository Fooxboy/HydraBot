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
            var kb = new KeyboardBuilder(bot);

            if (msg.Payload != null)
            {
                if (msg.Payload.Arguments != null)
                {
                    if (msg.Payload.Arguments[0] == "1")
                    {
                        kb.AddButton("Ford", "getcars", new List<string> { "Ford" });
                        kb.AddButton("GMC", "getcars", new List<string> { "GMC" });
                        kb.AddButton("Honda", "getcars", new List<string> { "Honda" });
                        kb.AddButton("Hummer", "getcars", new List<string> { "Hummer" });
                        kb.AddLine();
                        kb.AddButton("Infiniti", "getcars", new List<string> { "Infiniti" });
                        kb.AddButton("Jaguar", "getcars", new List<string> { "Jaguar" });
                        kb.AddButton("Jeep", "getcars", new List<string> { "Jeep" });
                        kb.AddButton("Kia", "getcars", new List<string> { "Kia" });
                        kb.AddLine();
                        kb.AddButton("◀", "autostore", color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                        kb.AddButton("▶", "autostore", new List<string> { "2" }, color: KeyboardButtonColor.Positive);
                    }
                    else if (msg.Payload.Arguments[0] == "2")
                    {
                        kb.AddButton("Lamborghini", "getcars", new List<string> { "Lamborghini" });
                        kb.AddButton("Land Rover", "getcars", new List<string> { "Land Rover" });
                        kb.AddButton("Lexus", "getcars", new List<string> { "Lexus" });
                        kb.AddButton("Marussia", "getcars", new List<string> { "Marussia" });
                        kb.AddLine();
                        kb.AddButton("Maserati", "getcars", new List<string> { "Maserati" });
                        kb.AddButton("McLaren", "getcars", new List<string> { "McLaren" });
                        kb.AddButton("Mercedes-Benz", "getcars", new List<string> { "Mercedes-Benz" });
                        kb.AddButton("Mitsubishi", "getcars", new List<string> { "Mitsubishi" });
                        kb.AddLine();
                        kb.AddButton("◀", "autostore", new List<string> { "1" }, color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                        kb.AddButton("▶", "autostore", new List<string> { "3" }, color: KeyboardButtonColor.Positive);
                    }
                    else if (msg.Payload.Arguments[0] == "3")
                    {
                        kb.AddButton("Nissan", "getcars", new List<string> { "Nissan" });
                        kb.AddButton("Pagani", "getcars", new List<string> { "Pagani" });
                        kb.AddButton("Pontiac", "getcars", new List<string> { "Pontiac" });
                        kb.AddButton("Porsche", "getcars", new List<string> { "Porsche" });
                        kb.AddLine();

                        kb.AddButton("Renault", "getcars", new List<string> { "Renault" });
                        kb.AddButton("Rolls-Royce", "getcars", new List<string> { "Rolls-Royce" });
                        kb.AddButton("Saab", "getcars", new List<string> { "Saab" });
                        kb.AddButton("Skoda", "getcars", new List<string> { "Skoda" });
                        kb.AddLine();
                        kb.AddButton("◀", "autostore", new List<string> { "2" }, color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                        kb.AddButton("▶", "autostore", new List<string> { "4" }, color: KeyboardButtonColor.Positive);
                    }
                    else if (msg.Payload.Arguments[0] == "4")
                    {
                        kb.AddButton("Subaru", "getcars", new List<string> { "Subaru" });
                        kb.AddButton("Tesla", "getcars", new List<string> { "Tesla" });
                        kb.AddButton("Toyota", "getcars", new List<string> { "Toyota" });
                        kb.AddButton("Volkswagen", "getcars", new List<string> { "Volkswagen" });
                        kb.AddLine();
                        kb.AddButton("Volvo", "getcars", new List<string> { "Volvo" });
                        kb.AddButton("Русский автопром", "getcars", new List<string> { "Rus" });
                        kb.AddLine();
                        kb.AddButton("◀", "autostore", new List<string> { "2" }, color: KeyboardButtonColor.Positive);
                        kb.AddButton("🏪 Магазин", "store");
                    }
                }
                else
                {
                    kb.AddButton("Aston Martin", "getcars", new List<string> { "Aston Martin" });
                    kb.AddButton("Audi", "getcars", new List<string> { "Audi" });
                    kb.AddButton("BMW", "getcars", new List<string> { "BMW" });
                    kb.AddButton("Bugatti", "getcars", new List<string> { "Bugatti" });
                    kb.AddLine();
                    kb.AddButton("Cadillac", "getcars", new List<string> { "Cadillac" });
                    kb.AddButton("Chevrolet", "getcars", new List<string> { "Chevrolet" });
                    kb.AddButton("Dodge", "getcars", new List<string> { "Dodge" });
                    kb.AddButton("Ferrari", "getcars", new List<string> { "Ferrari" });
                    kb.AddLine();
                    kb.AddButton("🏪 Магазин", "store");
                    kb.AddButton("▶", "autostore", new List<string> { "1" }, color: KeyboardButtonColor.Positive);

                }
            }
            sender.Text("🏎 Выберите производителя автомобиля на клавиатуре.", msg.ChatId, kb.Build());
        }
        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
