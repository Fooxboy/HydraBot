using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Commands.Bank;
using HydraBot.Helpers;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using HydraBot.Commands.Gang;

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
                
            }

            var kb = new KeyboardBuilder(bot);
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
