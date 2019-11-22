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
            Garages = new Garages();
            Gangs = new Gangs();
        }
        /// <summary>
        /// Работа с пользователями.
        /// </summary>
        public IUsersApi Users { get; set; }
        /// <summary>
        /// Работа с репортами
        /// </summary>
        public IReportsApi Reports { get; set; }
        /// <summary>
        /// Работа с гаражами
        /// </summary>
        public IGarageApi Garages { get; set; }
        /// <summary>
        /// Работа с бандами
        /// </summary>
        public IGangs Gangs { get; set; }
    }
}
