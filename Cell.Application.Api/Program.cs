using Cell.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cell.Application.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<AppDbContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var logger = services.GetService<ILogger<AppDbContextSeed>>();
                    new AppDbContextSeed(context)
                        .SeedAsync(context, env, logger)
                        .Wait();
                }).Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}