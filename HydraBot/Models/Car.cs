using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Text;

namespace HydraBot.Models
{
    public class Car
    {
        [Key] public long Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public long Power { get; set; }
        public long Weight { get; set; }
        public long Price { get; set; }
        public string Engine { get; set; }
        public long Health { get; set; }
        public bool IsUnderRepair { get; set; }
    }

    public class CarsJsonModel
    {
        public List<Car> cars { get; set; }
    }
}
