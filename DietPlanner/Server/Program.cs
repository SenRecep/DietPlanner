using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.Seed;
using DietPlanner.Shared.ExtensionMethods;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

namespace DietPlanner.Server
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                IHost host = CreateHostBuilder(args).Build();

                using IServiceScope serviceScope = host.Services.CreateScope();

                IServiceProvider services = serviceScope.ServiceProvider;

                DietPlannerDbContext configurationDbContext = services.GetRequiredService<DietPlannerDbContext>();
                UserRoleSeed seeder = services.GetRequiredService<UserRoleSeed>();
                configurationDbContext.Database.Migrate();
                await seeder.SeedAsync();

                Log.Information("Starting host...");
                await host.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>()
                        .ConfigureLogging((hostingContext, config) =>
                        {
                            config.ClearProviders();
                            config.AddSerilog(LoggerExtensionMethods.SerilogInit());
                        });
                    }).ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    });
    }
}
