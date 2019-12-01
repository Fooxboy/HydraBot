using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;

namespace HydraBot.Services
{
    public class ContributionService:INucleusService
    {
        public string Name => "Contribution service";
        public bool IsRunning { get; set; }
        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
            IsRunning = true;
            while (IsRunning)
            {
                Thread.Sleep(TimeSpan.FromDays(1));
                List<Contribution> contributions;
                using(var db = new Database())
                {
                    contributions =db.Contributions.ToList();
                }

                foreach (var contribution in contributions)
                {
                    using (var db = new Database())
                    {
                        var contr = db.Contributions.Single(c => c.UserId == contribution.UserId);
                        contr.CountDay -= 1;
                        contr.Money = contr.Money + ((contr.Money/100) * 7);
                        if (contr.CountDay <= 0)
                        {
                            var user = db.Users.Single(u => u.Id == contr.UserId);
                            user.MoneyInBank += contr.Money;
                            db.Contributions.Remove(contr);
                        }
                        db.SaveChanges();
                    }
                }
                
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}