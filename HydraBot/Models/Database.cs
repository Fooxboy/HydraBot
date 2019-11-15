using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Models
{
    public class Database: DbContext
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Репорты
        /// </summary>
        public DbSet<Report> Reports { get; set; }
        /// <summary>
        /// Гаражи
        /// </summary>
        public DbSet<Garage> Garages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=bot.db");
        }
    }
}
