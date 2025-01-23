using LDAP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using SqlServer;

namespace WinCC.AD.gRPCService
{
    [SupportedOSPlatform("windows")]
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Regedit.GetValue("BasePath", "C:\\workspace\\C#\\WinCCLibAD\\WinCC.AD.gRPCService"))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Configure logger                
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger(); 

            try
            {
                Log.Information("Application starting up.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "The application failed start correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSerilog()
                .ConfigureServices((context, services) => 
                {
                    services.Configure<LDAPConfigModel>(context.Configuration.GetSection("LDAPConfig"));
                    services.AddOptions();

                    services.AddSingleton<ILDAPSwitcher, LDAPSwitcher>();
                    services.AddTransient<ILogonProcessor, LogonProcessor>();
                    services.AddTransient<ISqlDataAccess, SqlDataAccess>();
                    services.AddTransient<ISqlProcessor, SqlProcessor>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel((context, options) =>
                    {
                        options.Configure(context.Configuration.GetSection("Kestrel"));
                    });
                    webBuilder.UseStartup<Startup>();
                });

    }
}
