using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands
{
    public class SettingsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var text = "⚙ Настройки";
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);

            if (user.SubOnNews)
                kb.AddButton("📰 Отписаться от рассылки", "unsubNewsLetter", color: KeyboardButtonColor.Negative);
            else kb.AddButton("📰 Подписаться на рассылку", "subOnNewsLetter", color: KeyboardButtonColor.Positive);
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }

        public string Command { get; }
        public string[] Aliases { get; }
    }
}