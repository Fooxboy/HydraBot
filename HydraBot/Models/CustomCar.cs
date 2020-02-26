using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class CustomCar
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public long Power { get; set; }
        public long Weight { get; set; }
        public bool IsAvaliable { get; set; }
        public bool IsModerate { get; set; }
        public long CarDatabaseId { get; set; }
    }
}