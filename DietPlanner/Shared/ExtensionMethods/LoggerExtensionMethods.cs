using System;

using DietPlanner.DTO.ExtensionMethods;
using DietPlanner.DTO.Response;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace DietPlanner.Shared.ExtensionMethods
{
    public static class LoggerExtensionMethods
    {
        public static Serilog.ILogger SerilogInit(bool isBlazor=false)
        {
            string logTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
            SelfLog.Enable(m => Console.Error.WriteLine(m));
            var conf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                //.WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: logTemplate);

            if (isBlazor)
                conf.WriteTo.BrowserConsole(outputTemplate: "[%cserilog{_}color:white;background:#8c7574;border-radius:3px;padding:1px 2px;font-weight:600;" + logTemplate);
            else
                conf.WriteTo.Console(outputTemplate: logTemplate, theme: AnsiConsoleTheme.Literate);
            return conf.CreateLogger(); 
        }

        public static void LogResponse<T, R>(this ILogger<T> logger, Response<R> response, string message = "") where R : class
        {
            if (!message.IsEmpty())
                logger.LogInformation(message);

            if (response.IsSuccessful && response.Data is string res)
                logger.LogInformation(res);

            if (!response.IsSuccessful)
            {
                string errors = response.ErrorData.GetErrors();
                if (response.StatusCode == StatusCodes.Status500InternalServerError)
                    logger.LogError(errors);
                else
                    logger.LogWarning(errors);
            }
        }
    }
}
