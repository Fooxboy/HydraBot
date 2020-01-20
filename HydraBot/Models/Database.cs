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
        /// <summary>
        /// Банды
        /// </summary>
        public DbSet<Gang> Gangs { get; set; }
        /// <summary>
        /// Автомобили
        /// </summary>
        public DbSet<Car> Cars { get; set; }
        /// <summary>
        /// Двигатели
        /// </summary>
        public DbSet<Engine> Engines { get; set; }
        /// <summary>
        /// Вложения
        /// </summary>
        public DbSet<Contribution> Contributions { get; set; }
        /// <summary>
        /// Номера автомобилей
        /// </summary>
        public DbSet<NumberCar> NumbersCars { get; set; }
        /// <summary>
        /// Гонки
        /// </summary>
        public DbSet<Race> Races { get; set; }
        /// <summary>
        /// Навыки
        /// </summary>
        public DbSet<Skills> Skillses { get; set; }
        /// <summary>
        /// Архив продаж авто.
        /// </summary>
        public DbSet<SellCar> SellCars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=bot.db");
        }
    }
}
