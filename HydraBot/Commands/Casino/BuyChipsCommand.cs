using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace HydraBot.Commands.Casino
{
    public class BuyChipsCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var text = "⚜ Покупка фишек для казино:" +
                       "♣ 1 фишка = 1000 рублей" +
                       "\n" +
                       "\n ❓ Укажите количество фишек, которое Вы хотите купить.";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("❌ Назад в казино", "casino");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "buychips";
        public string[] Aliases => new string[0];
    }
}