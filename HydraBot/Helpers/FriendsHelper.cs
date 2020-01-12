using System.Collections.Generic;
using System.Linq;

namespace HydraBot.Helpers
{
    public static class FriendsHelper
    {
        public static List<long> GetFriends(string friends)
        {
            var frs =friends.Split(";").ToList();
            if (frs[^1] == "") frs.Remove(frs[^1]);
            return frs.Select(e => e.ToLong()).ToList();
        }
    }
}