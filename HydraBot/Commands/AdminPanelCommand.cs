using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands
{
    public class AdminPanelCommand : INucleusCommand
    {
        public string Command => "admin";

        public string[] Aliases => new string[] { "Админ", "админка"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);

            if(user.Access < 4)
            {
                sender.Text("❌ Вам недоступна эта команда.", msg.ChatId);
                return;
            }
            var text = "❗ Админ панель. Доступные команды:" +
                "\n ▶| addmoney <ID> <количество> - Добавить донат рублей игроку. (Доступно с администратора) " +
                "\n ▶| addscore <ID> <количество> - Добавить опыта игроку. (Доступно с администратора)" +
                "\n ▶| fuel <ID> <количество> - Заправить бак игрока на <количество> топлива. (Доступно с администратора)" +
                "\n ▶| users - Вывод пользователей бота. (Доступно с администратора)" +
                "\n ▶| setaccess <ID> <привелегия> - Установить привелегию. (Доступно с администратора)" +
                "\n ➡- Доступные значения привелегий: " +
                "\n ➡➡0 - Игрок" +
                "\n ➡➡1 - VIP" +
                "\n ➡➡2 - Полицейский" +
                "\n ➡➡3 - Спонсор" +
                "\n ➡➡4 - Модератор" +
                "\n ➡➡5 - Старший модератор" +
                "\n ➡➡6 - Администратор";

            sender.Text(text, msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
