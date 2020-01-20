namespace HydraBot.Models
{
    public class SellCar
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public long OwnerId { get; set; }
        public long BuynerId { get; set; }
        public bool IsClose { get; set; }
    }
}