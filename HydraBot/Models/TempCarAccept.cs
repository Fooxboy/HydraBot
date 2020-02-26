namespace HydraBot.Models
{
    public class TempCarAccept
    {
        public long UserId { get; set; }
        public long CustomCarId { get; set; }
        public bool IsAccepted { get; set; }
    }
}