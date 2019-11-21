using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Models
{
    public class QuestionsDrivingSchool
    {
        public string Question { get; set; }
        public List<string> Responses { get; set; }
        public long Response { get; set; }
    }
}
