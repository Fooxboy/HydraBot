using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HydraBot.Helpers
{
    public class CarsHelper
    {
        private CarsHelper() { }
        private static CarsHelper _helper;

        public List<Car> Cars { get; set; }
        public static CarsHelper GetHelper()
        {
            _helper ??= new CarsHelper();
            return _helper;

        }

        public void InitCars(ILoggerService logger)
        {
            logger.Info("[CARS] Инициализация автомобилей...");
            var json = File.ReadAllText("CarsManifest.json");
            var model = JsonConvert.DeserializeObject<CarsJsonModel>(json).cars;
            Cars = model;
        }

    }
}
