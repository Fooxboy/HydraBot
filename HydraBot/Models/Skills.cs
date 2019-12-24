using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class Skills
    {
        [Key]
        public long UserId { get; set; }
        public long Driving { get; set; }
    }
}