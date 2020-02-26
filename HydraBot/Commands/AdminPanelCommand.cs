using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.NucleusBot;


namespace HydraBot.Commands
{
    public class AdminPanelCommand : INucleusCommand
    {
        public string Command => "admin";

        public string[] Aliases => new string[] { "Админ", "админка"};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb = new KeyboardBuilder(bot);
                kb.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb.Build());
            }
            
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
                "\n ➡➡6 - Администратор" +
                "\n ▶| addcar <ID> <idCar> - Выдает пользователю автомобиль из базы данных автомобилей." +
                "\n ▶| Профиль <ID> - Выводит профиль пользователя с ID" +
                "\n ▶| Рассылка <Текст> - Рассылка сообщений всем пользователям" +
                "\n ▶| Гараж <ID> - Выводит гараж пользователя с ID" +
                "\n ▶| Promocreate <рубли> <донат рубли> <опыт>- Создает прокод с количеством денег, донат рублей, опыта" +
                "\n➡- Пример: Promocreate 100 0 0 - создаст промокод на 100 рублей." +
                "\n ▶| Бан <ID> <время в часах> <причина> - банит игрока на <время в часах>." +
                "\n ▶| Разбан <ID> - снимает бан с игрока" +
                "\n ▶| Айди <VK ID> - получает айди пользователя в боте по его айди ВК." +
                "\n ▶| Баны - выводит список пользователей, которые сейчас в бане." +
                "\n ▶| Автопанель - выводит список запросов на кастомные авто.";

            sender.Text(text, msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }
    }
}
