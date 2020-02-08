using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Gang
{
    public class RenameCommand:INucleusCommand
    {
        public string Command => "renamegang";
        public string[] Aliases => new string[] {};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            var user = Main.Api.Users.GetUser(msg);
            UsersCommandHelper.GetHelper().Add("renamegang", user.Id);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("❌ Отменить", "gang");
            sender.Text("📃 Введите новое название Вашей банды", msg.ChatId, kb.Build());
        }

        public static string Rename(User user, string name)
        {
            var api = Main.Api;
            var gang = api.Gangs.GetGang(user.Gang);
            if (gang.Creator != user.Id) return "❌ Вы не являетесь создателем банды!";

            using (var db = new Database())
            {
                var g = db.Gangs.Single(gan => gan.Id == gang.Id);
                g.Name = name;
                db.SaveChanges();
            }

            UsersCommandHelper.GetHelper().Add("", user.Id);
            return $"✔ Имя банды изменено на {name}";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}