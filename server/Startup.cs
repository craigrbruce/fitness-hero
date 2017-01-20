using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Server.Models.Client;
using Server.Models;
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
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddAntiforgery(options => options.CookieName = options.HeaderName = "X-XSRF-TOKEN");
            services.AddOptions();
            services.AddScoped<IRepository<Client>, Repository<Client>>();

            services.AddMvcCore()
                .AddViews()
                .AddRazorViewEngine()
                .AddJsonFormatters(options => options.ContractResolver = new CamelCasePropertyNamesContractResolver());

            if (_environment.EnvironmentName != "Test")
            {
                services.AddAuthorization();
                services.AddCors();
            }

            services.AddIdentityServer()
              .AddTemporarySigningCredential()
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
              .AddInMemoryApiResources(Config.GetApiResources())
              .AddInMemoryClients(Config.GetClients())
              .AddAspNetIdentity<User>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{*url}", new { controller = "Home", action = "Index" });
            });
        }
    }
}
