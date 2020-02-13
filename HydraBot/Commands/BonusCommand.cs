using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class BonusCommand : INucleusCommand
    {
        public string Command => "bonus";

        public string[] Aliases => new[] {"бонус" };

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
            
            var user = Main.Api.Users.GetUser(msg);
            if(!user.IsAvailbleBonus)
            {
                sender.Text($"❌ Ежедневный бонус будет доступен через {user.TimeBonus} часа(часов)", msg.ChatId);
                return;
            }
            var text = string.Empty;
            Main.Api.Users.SetDayBonus(user.Id, user.BonusDay + 1);
            if(user.BonusDay == 1)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
                
            }else if(user.BonusDay == 2)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
            }
            else if(user.BonusDay == 3)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
            }
            else if(user.BonusDay == 4)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
            }
            else if(user.BonusDay == 5)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
            }
            else if(user.BonusDay == 6)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
            }
            else if(user.BonusDay == 7)
            {
                var countMoney = 100;
                Main.Api.Users.AddMoneyToBank(user.Id, countMoney);
                text = $"💳 Вы получили ежедневный бонус в размере {countMoney}. \n" +
                    $"❓ Приходите завтра и получите больше!";
                Main.Api.Users.SetDayBonus(user.Id, 0);
            }

            Main.Api.Users.SetIsAvalibleBonus(user.Id, false);
            Main.Api.Users.SetTimeBonus(user.Id, 24);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
