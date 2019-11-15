using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Interfaces
{
    /// <summary>
    /// API для упрощения работы с базой данных и другими частями бота.
    /// </summary>
    public interface IApi
    {
        /// <summary>
        /// Работа с пользователями.
        /// </summary>
        IUsersApi Users { get; set; }
        /// <summary>
        /// Репорты
        /// </summary>
        IReportsApi Reports { get; set; }
        /// <summary>
        /// Гаражи
        /// </summary>
        IGarageApi Garages { get; set; }
    }
}
