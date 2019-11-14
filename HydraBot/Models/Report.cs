using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Models
{
    public class Report
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string AnswerMessage { get; set; }
        public long FromId { get; set; }
        public long ModeratorId { get; set; }
        public bool IsAnswered { get; set; }
    }
}
