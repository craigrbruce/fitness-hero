using Microsoft.Extensions.Configuration;

namespace Server.Config
{
    public class DatabaseSettings
    {
        public DatabaseSettings(IConfigurationSection databaseConfigurationSection)
        {
            Host = databaseConfigurationSection["RDS_HOSTNAME"];
            Port = databaseConfigurationSection["RDS_PORT"];
            Database = databaseConfigurationSection["RDS_DB_NAME"];
            Username = databaseConfigurationSection["RDS_USERNAME"];
            Password = databaseConfigurationSection["RDS_PASSWORD"];
        }

        public string Host { get; }
        public string Port { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }

        public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
        
    }
}
