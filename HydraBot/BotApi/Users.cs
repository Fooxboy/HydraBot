using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Interfaces;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HydraBot.BotApi
{
    public class Users : IUsersApi
    {
        private readonly ILoggerService _logger;
        private readonly UsersHelper _helper;
        public Users(ILoggerService logger)
        {
            _logger = logger;
            _helper = new UsersHelper();
        }
        public bool AddUser(User user)
        {
            try
            {
                //подключение к бд
                using (var db = new Database())
                {
                    user.Id = db.Users.Count() + 1;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
               
                _logger.Info($"Добавлен новый пользователь: Имя - {user.Name} | Id - {user.Id}| VkId - {user.VkId}| TgId - {user.TgId}");
                return true;
            }catch(Exception e)
            {
                _logger.Error($"Произошла ошибка при добавлении нового пользователя: \n {e}");
                return false;
            }
           
        }

        public bool CheckUser(Message msg)
        {
            using(var db = new Database())
            {
                if (_helper.CheckIsIdVk(msg)) return db.Users.Any(u => u.VkId == _helper.GetUserIdVk(msg));
                else return db.Users.Any(u => u.TgId == msg.MessageTG.From.Id);
            }
        }

        public User GetUser(Message msg)
        {
            var user = _helper.CheckIsIdVk(msg) ? GetUserFromIdVk(_helper.GetUserIdVk(msg)) : GetUserFromIdTg(msg.MessageTG.From.Id);
            return user;
        }

        public User GetUserFromIdTg(long tgId)
        {
            using (var db = new Database())
            {
                var user = db.Users.Single(u => u.TgId == tgId);
                return user;
            }
        }

        public User GetUserFromIdVk(long vkId)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.VkId == vkId);
                return user;
            }
        }

        public bool RemoveUser(User user)
        {
            try
            {
                using (var db = new Database())
                {
                    db.Users.Remove(user);
                }
                _logger.Info($"Удален пользователь: {user.Name}");
                return true;
            }catch(Exception e)
            {
                _logger.Error($"Произошла ошибка при удалении пользователя с базы данных:  \n{e}");
                return false;
            }
            
        }

        public bool RemoveUser(Message msg)
        {
            var user = GetUser(msg);
            return RemoveUser(user);
        }
    }
}
