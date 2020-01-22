using System.Collections.Generic;

namespace HydraBot.Models
{
    public class TruckerItems
    {
        public List<TruckerItem> Items { get; set; }
    }

    public class TruckerItem
    {
        public string Item { get; set; }
        public long Weight { get; set; }
        public long Distance { get; set; }
        public long Money { get; set; }
        public long Time { get; set; }
    }
}