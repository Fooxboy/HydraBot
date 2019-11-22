using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HydraBot.Models
{
    public class Gang
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Members { get; set; }
        public long Creator { get; set; }
    }
}
