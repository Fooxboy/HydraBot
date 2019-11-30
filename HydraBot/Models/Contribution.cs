using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class Contribution
    {
        [Key]
        public long UserId { get; set; }
        public long Money { get; set; }
        public long CountDay { get; set; }
    }
}