using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace HydraBot.Models
{
    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public long Power { get; set; }
        public long Weight { get; set; }

    }

    public class am
    {
        public List<Car> Cars { get; set; }
    }
}
