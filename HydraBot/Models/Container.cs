using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class Container
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public long Price { get; set; }
        public long Weight { get; set; }
        public string LastNamePrice { get; set; }
        public long UserId { get; set; }
        public string Items { get; set; }
        public long Prize { get; set; }
    }
}