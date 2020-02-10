using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class Promocode
    {
        [Key]
        public long Id { get; set; }
        public string Text { get; set; }
        public long DonateMoney { get; set; }
        public long Money { get; set; }
        public long Experience { get; set; }
        public bool IsActivate { get; set; }
        public long CountActivations { get; set; }
    }
}