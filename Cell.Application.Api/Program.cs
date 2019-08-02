using Cell.Common.Extensions;
using Cell.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using NLog;
using NLog.Web;

namespace Cell.Application.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("INIT MAIN CELL APPLICATION");
                BuildWebHost(args)
                    .Initialize<AppDbContextSeed>()
                    .Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}