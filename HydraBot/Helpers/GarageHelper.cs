using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Helpers
{
    public class GarageHelper
    {
        private static GarageHelper _helper;
        private GarageHelper() { }

        public List<GarageBuyModel> Garages { get; set; }

        public static GarageHelper GetHelper()
        {
            _helper ??= new GarageHelper();
            return _helper;
        }

        public GarageBuyModel GetGarageModel(long id)
        {
            return Garages.Single(g => g.Id == id);
        }

        public void InitGarages(ILoggerService logger)
        {
            logger.Info("[GARAGES] Иницализация гаражей...");
            Garages = new List<GarageBuyModel>();

            Garages.Add(new GarageBuyModel() { Id = 0, CountPlaces = 1, Name = "Одноместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 1, CountPlaces = 2, Name = "Двуместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 2, CountPlaces = 3, Name = "Трехместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 3, CountPlaces = 4, Name = "Четырехместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 4, CountPlaces = 5, Name = "Пятиместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 5, CountPlaces = 6, Name = "Шестиместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 6, CountPlaces = 7, Name = "Семиместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 7, CountPlaces = 8, Name = "Восьмиместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 8, CountPlaces = 9, Name = "Девятиместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 9, CountPlaces = 10, Name = "Деятиместный гараж", Price = 1 });
            Garages.Add(new GarageBuyModel() { Id = 10, CountPlaces = 15, Name = "Гараж на 15 мест", Price = 1 });

            logger.Info($"[GARAGES] Загружено {Garages.Count} гаражей.");
        }
    }
}
