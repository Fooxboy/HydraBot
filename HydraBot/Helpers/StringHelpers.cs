using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Helpers
{
    public static class StringHelpers
    {
        public static long ToLong(this string str)
        {
            if (str != "")
            {
                return long.Parse(str);
            }

            return -1;

        }
    }
}
