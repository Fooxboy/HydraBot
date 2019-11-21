using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HydraBot.Services
{
    public class LevelService : INucleusService
    {
        public string Name => "Level Service";

        public bool IsRunning { get; set; }


        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
           
            IsRunning = true;
            while(IsRunning)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                List<User> users;
                using(var db = new Database())
                {
                    users =db.Users.ToList();
                }

                foreach (var user in users)
                {
                    var maxScore = user.Level * 150;
                    if(user.Score >= maxScore)
                    {
                        var api = Main.Api;
                        api.Users.RemoveScore(user.Id, user.Level * 150);
                        var lvl = api.Users.SetLevel(user.Id, user.Level + 1);

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
