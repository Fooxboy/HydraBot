using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Commands.Bank;
using HydraBot.Helpers;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using HydraBot.Commands.Friends;
using HydraBot.Commands.Gang;
using HydraBot.Commands.Garage;
using HydraBot.Commands.Race;
using HydraBot.Commands.Store;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class UnknownCommand : INucleusCommand
    {
        public string Command => "unknown";

        public string[] Aliases => null;

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var command = UsersCommandHelper.GetHelper().Get(user.Id);

            var text = string.Empty;
            if(command == "")
            {
                if (msg.ChatId > 2000000000) return;
                sender.Text("Неизвестная команда", msg.ChatId);
                return;
            }else if(command == "putrawmoney")
            {
                
                long count;
                try
                {
                    count = long.Parse(msg.Text);
                    text = PutCommand.PutMoney(user, count);
                }
                catch
                {
                    text = "❌ Вы ввели неверное число. Попробуйте ещё раз.";
                }
            }else if(command == "withdrawmoney")
            {
                long count;
                try
                {
                    count = long.Parse(msg.Text);
                    text = WithdrawCommand.Withdraw(user, count);
                }
                catch
                {
                    text = "❌ Вы ввели неверное число. Попробуйте ещё раз.";
                }
            }else if(command == "exchangedonate")
            {
                long count;
                try
                {
                    count = long.Parse(msg.Text);
                    text = ExchangeDonateCommand.Exchange(msg, count);
                }catch
                {
                    text = "❌ Вы ввели неверное число. Попробуйте ещё раз.";
                }
            }else if (command == "creategang")
            {
                 text = CreateCommand.Create(msg.Text, user.Id);
            }else if (command == "renamegang")
            {
                text = RenameCommand.Rename(user, msg.Text);
            }else if (command == "opencontribution")
            {
                try
                {
                    var array = msg.Text.Split(" ");

                    var count = long.Parse(array[0]);
                    var days = long.Parse(array[1]);
                    text = OpenContributionCommand.Open(user.Id, count, days);
                }
                catch
                {
                    text = "❌ Вы указали неверные числа";
                }
                
            }else if(command == "racefriend")
            {
                try
                {
                    var array = msg.Text.Split(" ");
                    var id = long.Parse(array[0]);
                    text = RaceFriendCommand.RunFriendBattle(user.Id, id, sender, bot, msg);
                }catch
                {
                    text = "Вы указали неверный id";
                }
            }else if (command == "addfriend")
            {
                try
                {
                    text = AddFriendCommand.AddFriend(user, msg.Text.ToLong(), sender);
                }
                catch
                {
                    text = "❌ Вы указали неверный Id.";
                }
            }else if (command == "removefriend")
            {
                try
                {

                    text = RemoveFriendCommand.RemoveFriend(user, msg.Text.ToLong());
                }
                catch
                {
                    text = "❌ Вы указали невеный Id.";
                }
            }else if (command == "sellcar")
            {
                try
                {
                    var array = msg.Text.Split(" ");
                    var idUser = long.Parse(array[0]);
                    var price = long.Parse(array[1]);

                    text = SellCarCommand.Sell(user, idUser, sender, price);
                }catch
                {
                    text = "❌ Произошла ошибка.";
                }
            }else if (command == "buycarnumber")
            {
                try
                {
                    var region = long.Parse(msg.Text);
                    if (region < 1 || region > 200) text = "❌ Регион находится за пределом допустимого значения.";
                    else
                    {
                        text = BuyCarNumberCommand.BuyNumber(user, region);
                    }
                }
                catch
                {
                    text = "❌ Произошла ошибка.";
                }
                
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
           // UsersCommandHelper.GetHelper().Add("", user.Id);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
