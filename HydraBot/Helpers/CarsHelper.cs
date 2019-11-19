using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            logger.Info($"[CARS] Загружено {model.Count} автомобилей.");
        }

        public Car GetCarFromId(long id)=> Cars.Single(c => c.Id == id);

        public List<Car> GetCarsFromId(List<long> ids)
        {
            var l = new List<Car>();
            foreach(var id in ids) l.Add(GetCarFromId(id));
            return l;
        }

        public List<Car> ConvertStringToCars(string ids)
        {
            var l = new List<Car>();
            var array = ids.Split(";");
            foreach(var element in array)
            {
                if (element == "") break;
                l.Add(GetCarFromId(long.Parse(element)));
            }

            return l;
        }

        public string AddCarToString(string ids, long carId)
        {
            return ids + $"{carId};";
        } 

    }
}
