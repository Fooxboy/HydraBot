using System.Collections.Generic;
using System.Linq;

namespace HydraBot.Helpers
{
    public static class FriendsHelper
    {
        public static List<long> GetFriends(string friends) => friends.Split(";").Select(e => e.ToLong()).ToList();
    }
}