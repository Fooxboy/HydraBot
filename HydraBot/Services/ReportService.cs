using Fooxboy.NucleusBot.Interfaces;
using HydraBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HydraBot.Services
{
    public class ReportService : INucleusService
    {
        public string Name => "Report Service";

        public bool IsRunning { get; set; }

        public static List<ReportsTimeModel> Times { get; set; }

        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
            IsRunning = true;

            //повторяется каждую минуту.
            Times = new List<ReportsTimeModel>();
            while(IsRunning)
            {
                Thread.Sleep(60000);
                foreach(var time in Times) if (!(time.Time == 0)) time.Time -= 1;


                //TODO: оптимизация.
                //if (Times.Count == 0) IsRunning = false;


            }
        }

        public static void AddToTimer(long userId)
        {
            
            Times.Add(new ReportsTimeModel() { Id = userId, Time = 5 });
            
        }

        public void Stop()
        {
            IsRunning = false;
            Times = null;
        }
    }
}
