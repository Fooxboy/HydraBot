using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HydraBot.Services
{
    public class BonusService : INucleusService
    {
        public string Name => "Bonus service";

        public bool IsRunning { get; set; }

        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
            while (IsRunning)
            {
                Thread.Sleep(TimeSpan.FromHours(1));
                using(var db = new Database())
                {
                    foreach(var user in db.Users)
                    {
                        if(!user.IsAvailbleBonus)
                        {
                            user.TimeBonus -= 1;
                            if (user.TimeBonus == 0) user.IsAvailbleBonus = true;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public void Stop()
        {
        }
    }
}
