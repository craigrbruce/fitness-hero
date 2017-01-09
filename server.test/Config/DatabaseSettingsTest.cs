using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using FluentAssertions;

using Server.Config;

namespace FH.Config.Tests
{
  public class DatabaseSettingsTest
  {

    [Fact]
    public void connection_string_should_be_correct()
    {
        var configurationSection = new Mock<IConfigurationSection>();
        configurationSection.SetupGet(x => x["RDS_HOSTNAME"]).Returns("hostname");
        configurationSection.SetupGet(x => x["RDS_PORT"]).Returns("port");
        configurationSection.SetupGet(x => x["RDS_DB_NAME"]).Returns("db");
        configurationSection.SetupGet(x => x["RDS_USERNAME"]).Returns("username");
        configurationSection.SetupGet(x => x["RDS_PASSWORD"]).Returns("password");

        var databaseSettings = new DatabaseSettings(configurationSection.Object);
        var actual = databaseSettings.ConnectionString;

        actual.ShouldBeEquivalentTo("Host=hostname;Port=port;Database=db;Username=username;Password=password");
    }
  }
}
