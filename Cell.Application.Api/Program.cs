using Cell.Common.Extensions;
using Cell.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Cell.Application.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .Initialize<AppDbContextSeed>()
                .Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}