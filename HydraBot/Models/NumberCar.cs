using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class NumberCar
    {
        [Key]
        public long Id { get; set; }
        public string Number { get; set; }
        public string Region { get; set; }
        public long CarId { get; set; }
        public long Owner {get;set;}
    }
}