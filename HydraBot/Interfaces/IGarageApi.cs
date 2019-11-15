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
        void RegisterGarage(Garage garage);

    }
}
