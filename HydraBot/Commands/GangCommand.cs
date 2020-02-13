using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class GangCommand : INucleusCommand
    {
        public string Command => "gang";

        public string[] Aliases => new string[] { };

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
            
            var api = Main.Api;
            var user = api.Users.GetUser(msg);
            var text = string.Empty;
            var kb = new KeyboardBuilder(bot);
            if(user.Gang  == 0)
            {
                text = "❌ Вы не являетесь участником команды. Но Вы можете создать свою!";
                kb.AddButton("➕ Создать команду (100.000 руб.)", "creategang");
                kb.AddLine();
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text(text, msg.ChatId, kb.Build());
                return;
            }

            var gang = api.Gangs.GetGang(user.Gang);
            var creatorUser = api.Users.GetUserFromId(gang.Creator);
            var helper = new UsersHelper();
            var creatorLink = helper.GetLink(creatorUser);

            text = $"👥 Название команды: {gang.Name}" +
                   $"\n 🔝 ID: {gang.Id}" +
                $"\n ⭐ Основатель: {creatorLink}" +
                $"\n 📖 Участники:";

            foreach(var m in gang.Members)
            {
                var member = api.Users.GetUserFromId(m);
                text += $"\n ▶ {helper.GetLink(member)}";
            }

            kb.AddButton("🔁 Переименовать команду", "renamegang");
            kb.AddLine();
            kb.AddButton("💱 Передать команду", "transfergang");
            kb.AddLine();
            kb.AddButton("❌ Удалить команду", "deletegang", color: KeyboardButtonColor.Negative);
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
