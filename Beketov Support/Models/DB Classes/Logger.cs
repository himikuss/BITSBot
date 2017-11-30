using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types;

namespace Beketov_Support.Models.DB_Classes
{
    
    public class Logger
    {
        BotDB db;
        
        public Logger()
        {
            db = new BotDB();
        }

        public void Msg(Message msg)
        {
            MessageLogs log = new MessageLogs
            {
                TextMessage = msg.Text,
                UserId = msg.From.Id,
                TimeStamp = DateTime.Now
            };

            db.MessageLogs.Add(log);
            db.SaveChanges();
        }

        public void Msg(string msg)
        {
            MessageLogs log = new MessageLogs
            {
                TextMessage = msg,
                UserId = 0,
                TimeStamp = DateTime.Now
            };

            db.MessageLogs.Add(log);
            db.SaveChanges();
        }
    }
}