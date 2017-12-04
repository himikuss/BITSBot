using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Beketov_Support.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Beketov_Support.Models
{
    public static class Logger
    {
        private static void Wright (string msg, string source, int userId, LogLevel level, LogType type)
        {
            using (BotDB db = new BotDB())
            {
                Log log = new Log
                {
                    Type = type,
                    Lvl = level,
                    TXT = msg,
                    TimeStamp = DateTime.Now
                };
                if (userId >= 0) log.UserId = userId;
                
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Ответ Бота
        /// </summary>
        /// <param name="msg">Текст ответа</param>
        public static void Wright (string msg)
        {
            Logger.Wright(msg, "", -1, LogLevel.Info, LogType.Message);
        }

        /// <summary>
        /// Сообщение пользователя
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public static void Wright (string msg, int userId)
        {
            Logger.Wright(msg, "", userId, LogLevel.Info, LogType.Message);
        }

        /// <summary>
        /// Отслеживание работы кода
        /// </summary>
        /// <param name="msg">Текст лога</param>
        /// <param name="source">Класс и метод</param>
        /// <param name="level">Уровень лога</param>
        public static void Wright (string msg, string source, LogLevel level)
        {
            Logger.Wright(msg, source, 0, level, LogType.Code);
        }

        /// <summary>
        /// Отслеживание работы Бота
        /// </summary>
        /// <param name="msg">Сообщение системы</param>
        /// <param name="source">Текущая операция</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="level">Уровень лога</param>
        public static void Wright (string msg, string source, int userId, LogLevel level)
        {
            Logger.Wright(msg, source, userId, level, LogType.System);
        }
    }
}