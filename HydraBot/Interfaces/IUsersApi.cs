﻿using Fooxboy.NucleusBot.Models;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Interfaces
{
    public interface IUsersApi
    {
        /// <summary>
        /// Получение экземляра класса пользователя по сообщению
        /// </summary>
        /// <param name="msg">сообщение</param>
        /// <returns>Пользователь</returns>
        User GetUser(Message msg);
        /// <summary>
        /// Получение пользователя по индентификатору Вконтакте
        /// </summary>
        /// <param name="vkId">Индентификатор ВКонтакте</param>
        /// <returns></returns>
        User GetUserFromIdVk(long vkId);
        /// <summary>
        /// Получение пользователя по индентификатору Телеграм
        /// </summary>
        /// <param name="tgId">Индентификатор телеграм</param>
        /// <returns></returns>
        User GetUserFromIdTg(long tgId);
        /// <summary>
        /// Удалить пользователя с базы данных
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns></returns>
        bool RemoveUser(User user);
        /// <summary>
        /// Удалить пользователя с базы данных
        /// </summary>
        /// <param name="msg">Сообщение пользователя</param>
        /// <returns></returns>
        bool RemoveUser(Message msg);
        /// <summary>
        /// Добавить пользователя в базу данных
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        bool AddUser(User user);
        /// <summary>
        /// Проверка регистрации.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckUser(Message msg);
    }
}
