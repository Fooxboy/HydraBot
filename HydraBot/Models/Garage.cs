using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HydraBot.Models
{
    public class Garage
    {
        [Key]
        public long UserId { get; set; }
        public long ParkingPlaces { get; set; }
        public List<Car> Cars { get; set; }
        public string Name { get; set; }
    }
}
