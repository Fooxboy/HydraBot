using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HydraBot.Models
{
    public class User
    {
        /// <summary>
        /// Индентификатор пользователя в боте.
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Индентификатор пользователя ВКонтакте. Если отсутсвует, тогда равен 0.
        /// </summary>
        public long VkId { get; set; }
        /// <summary>
        /// Индентификатор пользователя Телеграм. Если отсутсвует, тогда равен 0.
        /// </summary>
        public long TgId { get; set; }
        /// <summary>
        /// Полученный опыт пользователя
        /// </summary>
        public long Score { get; set; }
        /// <summary>
        /// Текущий уровень пользователя
        /// </summary>
        public long Level { get; set; }
        /// <summary>
        /// Забанен ли пользователь
        /// </summary>
        public bool IsBanned { get; set; }
        /// <summary>
        /// Указывается время бана в секундах. Если пользователь не забанен, тогда равен 0, если пользователь забанен навсегда равен -1.
        /// </summary>
        public long TimeBan { get; set; }

        /// <summary>
        /// Права доступа пользователя.
        /// </summary>
        public long Access { get; set; }
        /// <summary>
        /// Префикс пользователя.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Наличные деньги
        /// </summary>
        public long Money { get; set; }
        /// <summary>
        /// Деньги в банке
        /// </summary>
        public long MoneyInBank { get; set; }
        /// <summary>
        /// День бонуса
        /// </summary>
        public long BonusDay { get; set; }
        /// <summary>
        /// Время бонуса
        /// </summary>
        public long TimeBonus { get; set; }
        /// <summary>
        /// Доступен ли бонус
        /// </summary>
        public bool IsAvailbleBonus { get; set; }
        //Id бизнесов
        public string BusinessIds { get; set; }
        /// <summary>
        /// Количество Донат рублей.
        /// </summary>
        public long DonateMoney { get; set; }
        /// <summary>
        /// Список автоправ
        /// </summary>
        public string DriverLicense { get; set; }
        /// <summary>
        ///  Id команда
        /// </summary>
        public long Gang { get; set; }
        /// <summary>
        /// Id гонки, в которой участвует пользователь.
        /// </summary>
        public long Race { get; set; }
        /// <summary>
        /// Друзья.
        /// </summary>
        public string Friends { get; set; }
        /// <summary>
        /// Id пользователей, которые отправили запрос в друзья.
        /// </summary>
        public string FriendsRequests { get; set; }
        /// <summary>
        /// Нахоидтся на работе.
        /// </summary>
        public bool OnWork { get; set; }
        /// <summary>
        /// Подписан на рассылку.
        /// </summary>
        public bool SubOnNews { get; set; }
        public long Chips { get; set; }
    }
}
