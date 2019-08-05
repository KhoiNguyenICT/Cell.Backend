using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Cell.Api.Mappers;
using Cell.Application.Api.Mappers;
using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Service.Implementations;

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

        public static IServiceCollection AddMapper(this IServiceCollection service)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityMappingModel>();
                cfg.AddProfile<ModelMappingEntity>();
                cfg.AddProfile<ModelMappingModel>();
            });
            service.AddSingleton(mapperConfig.CreateMapper().RegisterMap());
            return service;
        }

        public static IServiceCollection ConfigIoc(this IServiceCollection service)
        {
            service.AddScoped<AppDbContextSeed>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddScoped<ISecurityPermissionService, SecurityPermissionService>();
            return service;
        }

    }
}