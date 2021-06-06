using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Containers.MicrosoftIOC;
using DietPlanner.ClientShared.Models;
using DietPlanner.ClientShared.Services;
using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Validation.FluentValidation.Auth;
using DietPlanner.Shared.ExtensionMethods;

using FluentValidation;

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Debugging;

namespace DietPlanner.Client
{
    public class Program
    {

        public static async Task<int> Main(string[] args)
        {
            try
            {
                WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

                builder.Logging.ClearProviders();
                builder.Logging.AddSerilog(LoggerExtensionMethods.SerilogInit(true));

                builder.RootComponents.Add<App>("#app");

                 AddConfiguration(builder);


                #region Add Services
                builder.Services.AddDependencies(builder.HostEnvironment.BaseAddress);
                #endregion



                Log.Information("Starting host...");

                var host = builder.Build();

                var storageService = host.Services.GetRequiredService<IUserStorageService>();
                var userStorage = host.Services.GetRequiredService<IUserStorage>();

                userStorage.User = await storageService.GetAsync();

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

        public static  void AddConfiguration(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(async p =>
            {
                var http = new HttpClient()
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                };
                return await http.GetFromJsonAsync<NavLinks>("navlinks.json")
                    .ConfigureAwait(false);
            });
        }
    }
}
