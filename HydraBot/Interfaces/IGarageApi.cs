using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Interfaces
{
    public interface IGarageApi
    {
        Garage GetGarage(Message msg);
        Garage GetGarage(long userId);
        string SetCars(long userId, string cars);
        void RegisterGarage(Garage garage);
        long AddFuel(long userId, long fuel);

        Garage UpgrateGarage(long userId, string name, long countPlaces, long idmodel);

    }
}
