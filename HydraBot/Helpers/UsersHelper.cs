using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
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


        public string GetLink(User user)
        {
            if(user.VkId == 0) return $"@id{user.TgId}";
            else return $"@id{user.VkId} ({user.Name})";
        }

        public string GetLink(Message msg)
        {
            if (msg.Platform == Fooxboy.NucleusBot.Enums.MessengerPlatform.Telegam) return $"@{msg.MessageTG.From.Username}";
            else
            {
                var id = GetUserIdVk(msg);
                var user = Main.Api.Users.GetUser(msg);
                return $"@id{id} ({user.Name})";
            }
        }
    }
}
