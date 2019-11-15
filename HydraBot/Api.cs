using Fooxboy.NucleusBot.Interfaces;
using HydraBot.BotApi;
using HydraBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot
{
    /// <summary>
    /// API для упрощения работы с базой данных и другими частями бота.
    /// </summary>
    public class Api : IApi
    {
        public Api(ILoggerService logger)
        {
            Users = new Users(logger);
            Reports = new Reports();
        }
        /// <summary>
        /// Работа с пользователями.
        /// </summary>
        public IUsersApi Users { get; set; }

        public IReportsApi Reports { get; set; }
        public IGarageApi Garages { get; set; }
    }
}
