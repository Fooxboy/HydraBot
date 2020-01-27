using System.ComponentModel.DataAnnotations;

namespace HydraBot.Models
{
    public class SellNumber
    {
        [Key]
        public long OperationId { get; set; }
        public long OwnerId { get; set; }
        public long NumberId { get; set; }
        public long BuynerId { get; set; }
        public long Price { get; set; }
        public bool IsClose { get; set; }

    }
}