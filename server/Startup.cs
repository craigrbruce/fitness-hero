using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Models;
using Server.Models.Client;
using Server.Persistence;

namespace Server
{
  public class Startup
  {
    private readonly IHostingEnvironment _environment;
    public Startup(IHostingEnvironment env)
    {
      _environment = env;
      Configuration = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile($"appsettings.json", optional: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables()
          .Build();
    }

    public IConfiguration Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAntiforgery(options => options.CookieName = options.HeaderName = "X-XSRF-TOKEN");
      services.AddOptions();
      services.AddScoped<IRepository<Client>, Repository<Client>>();

      if (_environment.IsEnvironment("Test"))
      {
        services.AddDbContext<DatabaseContext>(
            options => options.UseSqlite("Data Source=:memory:"),
            ServiceLifetime.Singleton);
      }
      else
      {
        services.AddDbContext<DatabaseContext>(options =>
        {
          var dbConfig = Configuration.GetSection("Database");
          options.UseNpgsql($@"Host={dbConfig["RDS_HOSTNAME"]};
                        Port={dbConfig["RDS_PORT"]};
                        Database={dbConfig["RDS_DB_NAME"]};
                        Username={dbConfig["RDS_USERNAME"]};
                        Password={dbConfig["RDS_PASSWORD"]}");
        });
      }

      services.AddIdentity<User, IdentityRole>()
          .AddEntityFrameworkStores<DatabaseContext>()
          .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
        options.User.RequireUniqueEmail = true;
        options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
        {
          OnSignedIn = ctx =>
          {
            services.Configure<Application>(application =>
            {
              var idClaim = ctx.Principal.FindFirst(options.ClaimsIdentity.UserIdClaimType);
              application.LoggedInUserId = idClaim.Value;
            });

            return System.Threading.Tasks.Task.FromResult(0);
          }

        };
      });

      services.AddMvcCore()
          .AddAuthorization()
          .AddViews()
          .AddRazorViewEngine()
          .AddJsonFormatters();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
    {
      factory.AddConsole(Configuration.GetSection("Logging"));
      factory.AddDebug();

      app.UseStaticFiles();
      app.UseIdentity();

      if (!string.IsNullOrEmpty(Configuration["Authentication:Facebook:AppId"]))
      {
        app.UseFacebookAuthentication(new FacebookOptions
        {
          AppId = Configuration["Authentication:Facebook:AppId"],
          AppSecret = Configuration["Authentication:Facebook:AppSecret"],
          Scope = { "email" },
          Fields = { "name", "email" },
          SaveTokens = true,
        });
      }

      app.UseMvc(routes =>
      {
        routes.MapRoute("default", "{*url}", new { controller = "Home", action = "Index" });
      });
    }
  }
}
