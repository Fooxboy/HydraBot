using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.BotApi
{
    public class Reports:IReportsApi
    {

        public List<Report> GetReports()
        {
            var list = new List<Report>();
            list = GetAllReports().Where(r => r.IsAnswered == false).ToList();

            return list;
        }


        public List<Report> GetAllReports()
        {
            using (var db = new Database()) return db.Reports.ToList();
        }

        public bool AddReport(string message, long id)
        {
            var rep = new Report();
            rep.FromId = id;
            rep.IsAnswered = false;
            rep.Message = message;
            rep.ModeratorId = 0;
            AddReport(rep);
            return true;
        }

        public bool AddReport(Report rep)
        {
            try
            {
                using (var db = new Database())
                {
                    rep.Id = db.Reports.Count() + 1;
                    db.Reports.Add(rep);
                    db.SaveChanges();
                }
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public Report GetReportFromId(long reportId)
        {
            using(var db = new Database())
            {
                var rep =db.Reports.Single(r => r.Id == reportId);
                return rep;
            }
        }

        public bool SetReportInfo(long reportId, long adminId, string answerMessage)
        {
            using(var db = new Database())
            {
                var rep = db.Reports.Single(r => r.Id == reportId);
                rep.ModeratorId = adminId;
                rep.IsAnswered = true;
                rep.AnswerMessage = answerMessage;
                db.SaveChanges();
            }
                
        }
    }
}
