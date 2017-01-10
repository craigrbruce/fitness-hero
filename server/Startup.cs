// Copyright © 2014-present Kriasoft, LLC. All rights reserved.
// This source code is licensed under the MIT license found in the
// LICENSE.txt file in the root directory of this source tree.

using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Models;

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

            if (_environment.IsEnvironment("Test"))
            {
                services.AddDbContext<DatabaseContext>(
                    options => options.UseSqlite("Data Source=:memory:"),
                    ServiceLifetime.Singleton);
            }
            else
            {
                // Register Entity Framework database context
                // https://docs.efproject.net/en/latest/platforms/aspnetcore/new-db.html
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
        public static void Main()
        {
            var cwd = Directory.GetCurrentDirectory();
            var web = Path.GetFileName(cwd) == "server" ? "../public" : "public";

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot(web)
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
