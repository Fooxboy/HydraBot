using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Models
{
    public class UserCommand
    {
        public long UserId { get; set; }
        public string PreviousCommand { get; set; }
    }
}
