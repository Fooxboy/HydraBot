using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;

namespace HydraBot.Services
{
    public class BusinessService :INucleusService
    {
        public string Name => "Buisines Service";
        public bool IsRunning { get; set; }
        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
            IsRunning = true;
            while (IsRunning)
            {
                Thread.Sleep(TimeSpan.FromHours(1));
                using (var db = new Database())
                {
                    var users = db.Users.ToList();
                    foreach (var user in users)
                    {
                        if (user.BusinessIds != "")
                        {
                            var ids = user.BusinessIds.Split(",");
                            foreach (var id in ids)
                            {
                                if (id == "1") Main.Api.Users.AddMoney(user.Id, 100);
                                if (id == "2") Main.Api.Users.AddMoney(user.Id, 100);
                                if (id == "3") Main.Api.Users.AddMoney(user.Id, 100);
                                if (id == "4") Main.Api.Users.AddMoney(user.Id, 100);
                                if (id == "5") Main.Api.Users.AddMoney(user.Id, 100);
                                if (id == "6") Main.Api.Users.AddMoney(user.Id, 100);
                            }
                        }
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