using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Client;

namespace Server.Persistence
{
  public class DatabaseContext : IdentityDbContext<User>
  {
    public DatabaseContext(DbContextOptions options, IHostingEnvironment env = null) : base(options)
    {
      if (env != null)
      {
        if (env.EnvironmentName == "Test")
        {
          Database.OpenConnection();
          Database.EnsureCreated();
        }
        else if (env.IsDevelopment())
        {
          Database.SetCommandTimeout(300);
          Database.Migrate();
        }
      }

    }

    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Client>().ToTable("Clients");
    }
  }
}
