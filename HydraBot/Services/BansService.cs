using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;

namespace HydraBot.Services
{
    public class BansService:INucleusService
    {
        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
            IsRunning = true;
            while (IsRunning) 
            {
                Thread.Sleep(TimeSpan.FromHours(1));
                using (var db = new Database())
                {
                    var users = db.Users.Where(u => u.IsBanned);
                    foreach (var user in users)
                    {
                        user.TimeBan -= 1;
                        if (user.TimeBan == 0) user.IsBanned = false;
                    }
                    
                    db.SaveChanges();
                }
            }
        }

        public void Stop()
        {
        }

        public string Name => "bans service";
        public bool IsRunning { get; set; }
    }
}