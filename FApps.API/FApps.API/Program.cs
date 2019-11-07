using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace FApps.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateMSSqlLogger();
            CreateWebHostBuilder(args).Build().Run();            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();

        private static void CreateMSSqlLogger()
        {
            Log.Logger = new LoggerConfiguration()
               //.MinimumLevel.Verbose()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.RollingFile(@"./logs/ErrorLog-{Date}.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level}:{EventId} [{SourceContext}] {Message}{NewLine}{Exception}")
               .WriteTo.MSSqlServer("server=.\\SQLEXPRESS;database=projectDB;Trusted_Connection=True;", "LogEntries", autoCreateSqlTable: true)
               .CreateLogger();
        }
    }
}
