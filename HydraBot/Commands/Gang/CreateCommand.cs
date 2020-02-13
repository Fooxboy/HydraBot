using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Gang
{
    public class CreateCommand : INucleusCommand
    {
        public string Command => "creategang";
        public string[] Aliases => new string[] {};
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
            var price = 100000;
            var kb = new KeyboardBuilder(bot);

            if (user.Money < price)
            {
                kb.AddButton(ButtonsHelper.ToHomeButton());
                sender.Text("❌ У Вас недостаточно наличных денег для создания банды.", msg.ChatId, kb.Build());
                return;
            }
            
            UsersCommandHelper.GetHelper().Add("creategang", user.Id);
            kb.AddButton("❌ Отменить", "menu", color: KeyboardButtonColor.Negative);
            sender.Text("👥 Напишите название Вашей банды", msg.ChatId, kb.Build());

        }

        public static string Create(string name, long creator)
        {
            try
            {
                var api = Main.Api;
                var gang = api.Gangs.CreateGang(creator, name);

                using (var db = new Database())
                {
                    var user = db.Users.Single(u => u.Id == creator);
                    user.Gang = gang.Id;
                    db.SaveChanges();
                }

                UsersCommandHelper.GetHelper().Add("", creator);
                return $"👥 Банда {name} создана!";
            }
            catch
            {
                UsersCommandHelper.GetHelper().Add("", creator);
                return "❌ Мы не смогли создать банду из-за системной ошибки!";
            }
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}