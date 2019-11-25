using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HydraBot.Models
{
    public class Garage
    {
        [Key]
        public long UserId { get; set; }
        public long GarageModelId { get; set; } 
        public long ParkingPlaces { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPhone { get; set; }
        public string Cars { get; set; }
        public string Engines { get; set; }
        public long SelectCar { get; set; }
        public string Name { get; set; }
        public long Fuel { get; set; }
    }
}
