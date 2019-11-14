using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Helpers
{
    public class UsersCommandHelper
    {
        public List<UserCommand> UsersCommand { get; set; }

        private UsersCommandHelper()
        {
            UsersCommand = new List<UserCommand>();
        }
        private static UsersCommandHelper _helper;

        public static UsersCommandHelper GetHelper()
        {
            _helper ??= new UsersCommandHelper();
            return _helper;
        }

        public void Add(string command, long userId)
        {
            if(UsersCommand.Any(c => c.UserId == userId))
            {
                var comm = UsersCommand.Single(c => c.UserId == userId);
                comm.PreviousCommand = command;
            }else
            {
                UsersCommand.Add(new UserCommand() { PreviousCommand = command, UserId = userId });
            }
        }

        public string Get(long userId)
        {
            if (UsersCommand.Any(c => c.UserId == userId))
            {
                var comm = UsersCommand.Single(c => c.UserId == userId);
                return comm.PreviousCommand;
            }
            else return "";
        }
    }
}
