using Microsoft.Extensions.Configuration;

namespace Server.Config
{
    public class DatabaseSettings
    {
        public DatabaseSettings(IConfigurationSection databaseConfigurationSection)
        {
            this.Host = databaseConfigurationSection.GetValue<string>("RDS_HOSTNAME");
            this.Port = databaseConfigurationSection.GetValue<string>("RDS_PORT");
            this.Database = databaseConfigurationSection.GetValue<string>("RDS_DB_NAME");
            this.Username = databaseConfigurationSection.GetValue<string>("RDS_USERNAME");
            this.Password = databaseConfigurationSection.GetValue<string>("RDS_PASSWORD");
        }

        public string Host { get; }
        public string Port { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }

        public string ConnectionString {
            get {
                return "Host=" + Host + ";Port=" + Port + ";Database=" + Database + ";Username=" + Username + ";Password=" + Password;
            }
        }
    }
}
