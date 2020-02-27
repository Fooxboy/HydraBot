using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class MessageInfo
    {
        [Key]
        public long UserId { get; set; }
        public string LastMessageUsers { get; set; }
        public string LastMessageText { get; set; }
    }
}