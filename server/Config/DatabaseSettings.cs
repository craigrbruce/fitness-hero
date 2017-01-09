using Microsoft.Extensions.Configuration;

namespace Server.Config
{
    public class DatabaseSettings
    {
        public DatabaseSettings(IConfigurationSection databaseConfigurationSection)
        {
            Host = databaseConfigurationSection.GetValue<string>("RDS_HOSTNAME");
            Port = databaseConfigurationSection.GetValue<string>("RDS_PORT");
            Database = databaseConfigurationSection.GetValue<string>("RDS_DB_NAME");
            Username = databaseConfigurationSection.GetValue<string>("RDS_USERNAME");
            Password = databaseConfigurationSection.GetValue<string>("RDS_PASSWORD");
        }

        public string Host { get; }
        public string Port { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }

        public string ConnectionString => "Host=" + Host + ";Port=" + Port + ";Database=" + Database + ";Username=" + Username + ";Password=" + Password;
        
    }
}
