using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.Gang
{
    public class CreateCommand : INucleusCommand
    {
        public string Command => "creategang";
        public string[] Aliases => new string[] {};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
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

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}