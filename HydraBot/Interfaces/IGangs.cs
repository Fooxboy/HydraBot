using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Interfaces
{
    public interface IGangs
    {
        Gang GetGang(long id);
        void AddMember(long id, long userId);
        List<long> GetMembers(long id);
        void RemoveMember(long id, long userId);
        Gang CreateGang(long creator, string name);
        void SetCreator(long id, long userId);
    }
}
