using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Helpers
{
    public class UsersHelper
    {

        public bool CheckIsIdVk(Message msg) => msg.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Vkontakte;

        public long GetUserIdVk(Message msg)
        {
            if (msg.ChatId < 2000000000) return msg.ChatId;
            else return msg.MessageVK.FromId.Value;
        }
    }
}
