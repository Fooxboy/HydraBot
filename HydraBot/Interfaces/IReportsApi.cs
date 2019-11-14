using HydraBot.BotApi;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Interfaces
{
    public interface IReportsApi
    {
        List<Report> GetReports();
        List<Report> GetAllReports();

        bool AddReport(string message, long id);
        bool AddReport(Report rep);
        Report GetReportFromId(long reportId);
        bool SetReportInfo(long reportId, long adminId, string answerMessage);
    }
}
