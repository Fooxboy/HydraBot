using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HydraBot.Commands
{
    public class SetNickCommand : INucleusCommand
    {
        public string Command => "nick";

        public string[] Aliases => new[] { "ник", "никнейм" };

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("", user.Id);

            var nickname = string.Empty;
            var array = msg.Text.Split(" ");
            for (int i = 1; i < array.Length; i++) nickname += $"{array[i]}";
                //msg.Text.Replace("nick", "").Replace("ник", "").Replace("никнейм", "");
            
            var text = string.Empty;
            var isAvaible = true;

            if(nickname.Length < 4)
            {
                text = "❌ Вы не можете установить меньше четырех символов";
                isAvaible = false;
            }

            if(user.Access == 0 && nickname.Length > 15)
            {
                text = "❌ Вы не можете установить ник длинее 15 символов.";
                isAvaible = false;
            }

            if((user.Access == 1 || user.Access ==2) && nickname.Length > 20)
            {
                text = "❌ Вы не можете установить ник длинее 20 символов.";
                isAvaible = false;
            }

            if ((user.Access == 3 || user.Access == 4 || user.Access ==5) && nickname.Length > 25)
            {
                text = "❌ Вы не можете установить ник длинее 25 символов.";
                isAvaible = false;
            }
            
            if(isAvaible)
            {
               var result = Main.Api.Users.SetNickname(user, nickname);
                if (result) text = $"✔ Ваш никнейм успешно сменен на {nickname}";
                else text = $"❌ Мы не смогли сменить никнейм из-за системной ошибки.";
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
