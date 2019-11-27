using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class Engine
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long Power { get; set; }
        public long Weight { get; set; }
        public long CarId { get; set; }
    }
}