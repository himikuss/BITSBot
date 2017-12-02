namespace Beketov_Support.Models
{
    using Beketov_Support.Models.Entities;
    using System.Data.Entity;

    public enum LogType { Message, Code, System };
    public enum LogLevel { Info, Warranty, Error};
    public enum UserRole { User, Operator, DChief, Admin };
    public enum MessageType { Info, Condition, Script };
    public enum SParamType { Text, Number, IP }

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
            Database.SetInitializer<BotDB>(new DropCreateDatabaseIfModelChanges<BotDB>());
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Button> Buttons { get; set; }
        public virtual DbSet<Script> Scripts { get; set; }
        public virtual DbSet<ScriptParam> ScriptParams { get; set; }
    }
}