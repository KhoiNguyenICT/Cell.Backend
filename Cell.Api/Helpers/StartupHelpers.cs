using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cell.Common.Constants;
using Cell.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Cell.Api.Helpers
{
    public static class StartupHelpers
    {

        private static readonly string AssemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        public static IServiceCollection AddCustomDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
            service.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, b =>
                    {
                        b.MigrationsAssembly(AssemblyName);
                        b.MigrationsHistoryTable("__EFMigrationsHistory");
                    }));
            return service;
        }

        public static IServiceCollection ConfigIoc(this IServiceCollection service)
        {
            service.AddScoped<AppDbContextSeed>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return service;
        }

    }
}