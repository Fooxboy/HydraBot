using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Bank
{
    public class CloseContributionCommand : INucleusCommand
    {
        public string Command => "closecontribution";
        public string[] Aliases => new string[0];
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("↩ Назад к вкладам", "contribution");
            long money = 0;
            using (var db = new Database())
            {
                var contr = db.Contributions.Single(c => c.UserId == user.Id);
                 money = contr.Money;
                 db.Contributions.Remove(contr);
            }

            Main.Api.Users.AddMoneyToBank(user.Id, money);
            sender.Text($"💵 На Ваш банковский счет зачислено {money} руб.", msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}