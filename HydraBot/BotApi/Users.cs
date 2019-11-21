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
using System.Threading.Tasks;
using VkNet.Exception;

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

        public long AddDonateMoney(long userId, long count)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.DonateMoney += count;
                db.SaveChanges();
                return user.DonateMoney;
            }
        }

        public long AddMoney(long userId, long money)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.Money += money;
                db.SaveChanges();

                return user.Money;
            }
        }

        public long AddMoneyToBank(long userId, long money)
        {
            using (var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.MoneyInBank += money;
                db.SaveChanges();

                return user.MoneyInBank;
            }
        }

        public long AddUser(User user)
        {
            try
            {
                long userId;
                //подключение к бд
                using (var db = new Database())
                {
                    user.Id = db.Users.Count() + 1;
                    userId = user.Id;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
               
                _logger.Info($"Добавлен новый пользователь: Имя - {user.Name} | Id - {user.Id}| VkId - {user.VkId}| TgId - {user.TgId}");
                return userId;
            }catch(Exception e)
            {
                _logger.Error($"Произошла ошибка при добавлении нового пользователя: \n {e}");
                return 0;
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

        public bool CheckUser(long userId)
        {
            using(var db = new Database())
            {
                return db.Users.Any(u => u.Id == userId);
            }
            //throw new NotImplementedException();
        }

        public User GetUser(Message msg)
        {
            var user = _helper.CheckIsIdVk(msg) ? GetUserFromIdVk(_helper.GetUserIdVk(msg)) : GetUserFromIdTg(msg.MessageTG.From.Id);
            return user;
        }

        public User GetUserFromId(long id)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == id);
                return user;
            }
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

        public long RemoveDonateMoney(long userId, long count)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.DonateMoney -= count;
                db.SaveChanges();
                return user.DonateMoney;
            }
        }

        public long RemoveMoney(long userId, long money)
        {
            using (var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.Money -= money;
                db.SaveChanges();

                return user.Money;
            }
        }

        public long RemoveMoneyToBank(long userId, long money)
        {
            using (var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.MoneyInBank -= money;
                db.SaveChanges();

                return user.MoneyInBank;
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

        public long SetDayBonus(long userId, long count)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.BonusDay = count;
                db.SaveChanges();
                return user.BonusDay;
            }
        }

        public bool SetIsAvalibleBonus(long userId, bool value)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.IsAvailbleBonus = value;
                db.SaveChanges();
                return user.IsAvailbleBonus;
            }
        }

        public bool SetNickname(User user, string nickname)
        {
            try
            {
                using (var db = new Database())
                {
                    var us = db.Users.Single(u => u.Id == user.Id);
                    us.Name = nickname;
                    db.SaveChanges();
                }

                return true;
            }catch(Exception e)
            {
                _logger.Error($"Произошла ошибка при смене никнейма: \n {e}");
                return false;
            }
        }

        public long SetTimeBonus(long userId, long count)
        {
            using(var db = new Database())
            {
                var user = db.Users.Single(u => u.Id == userId);
                user.TimeBonus = count;
                db.SaveChanges();
                return user.TimeBonus;
            }
        }
    }
}
