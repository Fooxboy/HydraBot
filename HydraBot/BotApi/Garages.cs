using Fooxboy.NucleusBot.Models;
using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HydraBot.BotApi
{
    public class Garages : IGarageApi
    {
        public long AddFuel(long userId, long fuel)
        {
            using(var db = new Database())
            {
                var gar = db.Garages.Single(g => g.UserId == userId);
                gar.Fuel += fuel;
                db.SaveChanges();
                return gar.Fuel;
            }
        }

        public Garage GetGarage(Message msg)
        {
            var user = Main.Api.Users.GetUser(msg);
            return GetGarage(user.Id);
            
        }

        public Garage GetGarage(long userId)
        {
            using(var db = new Database())
            {
                var garage = db.Garages.Single(g => g.UserId == userId);
                return garage;
            }
        }

        public void RegisterGarage(Garage garage)
        {
            using(var db = new Database())
            {
                db.Garages.Add(garage);
                db.SaveChanges();
            }
        }

        public string SetCars(long userId, string cars)
        {
            using(var db = new Database())
            {
                var gar = db.Garages.Single(g => g.UserId == userId);
                gar.Cars = cars;
                db.SaveChanges();
                return gar.Cars;
            }
        }

        public Garage UpgrateGarage(long userId, string name, long countPlaces, long idmodel)
        {
            using(var db = new Database())
            {
                var garage = db.Garages.Single(g => g.UserId == userId);
                garage.Name = name;
                garage.ParkingPlaces = countPlaces;
                garage.GarageModelId = idmodel;
                db.SaveChanges();
                return garage;
            }
        }
    }
}
