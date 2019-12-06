using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class Race
    {
        [Key]
        public long Id { get; set; }
        public long Creator { get; set; }
        public long Enemy { get; set; }
        public bool IsRequest { get; set; }
        public long Winner { get; set; }
    }
}