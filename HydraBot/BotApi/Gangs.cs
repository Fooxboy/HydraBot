using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.BotApi
{
    public class Gangs : IGangs
    {
        public void AddMember(long id, long userId)
        {
            using(var db = new Database())
            {
                var gang = db.Gangs.Single(g => g.Id == id);
                gang.Members += $"{userId},";
                db.SaveChanges();
            }
        }

        public Gang CreateGang(long creator, string name)
        {
            using(var db = new Database())
            {
                var gang = new Gang()
                {
                    Members = "",
                    Name = name,
                    Creator = creator,
                    Id = db.Gangs.Count() + 1
                };
                db.Gangs.Add(gang);
                db.SaveChanges();
                return gang;
                
            }
        }

        public Gang GetGang(long id)
        {
            using (var db = new Database())
            {
                var gang = db.Gangs.Single(g => g.Id == id);
                return gang;
            }
        }

        public List<long> GetMembers(long id)
        {
            var members = new List<long>();
            using (var db = new Database())
            {
                var gang = db.Gangs.Single(g => g.Id == id);
                var mem = gang.Members.Split(",");
                foreach (var member in mem) members.Add(long.Parse(member));
            }

            return members;
        }

        public void RemoveMember(long id, long userId)
        {
            using (var db = new Database())
            {
                var gang = db.Gangs.Single(g => g.Id == id);
                gang.Members = gang.Members.Replace($"{userId},", "");
                db.SaveChanges();
            }
        }

        public void SetCreator(long id, long userId)
        {
            using (var db = new Database())
            {
                var gang = db.Gangs.Single(g => g.Id == id);
                gang.Creator = userId;
                db.SaveChanges();
            }
        }
    }
}
