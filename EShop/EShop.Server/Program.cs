using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EShop.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                   {
                       // The ILoggingBuilder minimum level determines the
                       // the lowest possible level for logging. The log4net
                       // level then sets the level that we actually log at.
                       logging.AddLog4Net();
                       logging.SetMinimumLevel(LogLevel.Debug);
                   })
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
