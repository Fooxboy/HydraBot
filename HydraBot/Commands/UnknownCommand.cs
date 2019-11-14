using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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

            if(command == "")
            {
                if (msg.ChatId > 2000000000) return;
                sender.Text("Неизвестная команда", msg.ChatId);
            }else if(command == "putrawmoney")
            {
                //todo: put
            }else if(command == "withdrawmoney")
            {
                //todo: withdraw
            }
            
            //throw new NotImplementedException();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            //throw new NotImplementedException();
        }
    }
}
