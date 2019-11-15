using Fooxboy.NucleusBot.Models;
using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.BotApi
{
    public class Garages : IGarageApi
    {
        public Garage GetGarage(Message msg)
        {
            var user = Main.Api.Users.GetUser(msg);
            using(var db = new Database())
            {
                var garage = db.Garages.Single(g => g.UserId == user.Id);
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
    }
}
