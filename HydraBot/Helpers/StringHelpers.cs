using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Helpers
{
    public static class StringHelpers
    {
        public static long ToLong(this string str)=> long.Parse(str);
    }
}
