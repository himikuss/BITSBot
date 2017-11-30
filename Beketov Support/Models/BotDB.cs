namespace Beketov_Support.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BotDB : DbContext
    {
        // Контекст настроен для использования строки подключения "BotDB" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Beketov_Support.Models.BotDB" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "BotDB" 
        // в файле конфигурации приложения.
        public BotDB()
            : base("name=BotDB")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<MessageLogs> MessageLogs { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Buttons> Buttons { get; set; }
        public virtual DbSet<Scripts> Scripts { get; set; }
        public virtual DbSet<ScriptLinks> ScriptLinks { get; set; }
    }

    public class MessageLogs
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }
        public string TextMessage { get; set; }
    }

    public class Logs
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Author { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
    }

    public class Users
    {
        public int Id { get; set; }
        public string TelegramId { get; set; }
        public int Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebUserName { get; set; }
        public string WebPassword { get; set; }
        public string EMail { get; set; }
        public string Company { get; set; }
    }

    public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
    }

    public class Buttons
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string Text { get; set; }
        public int NMessage { get; set; }
        public string Description { get; set; }
    }

    public class Scripts
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }

    public class ScriptLinks
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int ScriptId { get; set; }
    }
}