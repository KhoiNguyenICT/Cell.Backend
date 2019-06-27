using AutoMapper;
using Cell.Application.Api.Mappers;
using Cell.Core.Constants;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.BasedTableAggregate;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using Cell.Domain.Aggregates.SettingAdvancedAggregate;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Cell.Domain.Aggregates.SettingViewAggregate;
using Cell.Infrastructure;
using Cell.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Cell.Core.SeedWork;

namespace Cell.Application.Api.Helpers
{
    public static class StartupHelpers
    {
        private static readonly string AssemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
            services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, b =>
                    {
                        b.MigrationsAssembly(AssemblyName);
                        b.MigrationsHistoryTable("__EFMigrationsHistory");
                    }));
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            services.AddSingleton(mapperConfig.CreateMapper().RegisterMap());
            return services;
        }

        public static IServiceCollection ConfigSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v5", new Info { Title = "CELL APIs", Version = "5.0.0" });
            });
            return service;
        }

        public static IApplicationBuilder Swagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v5/swagger.json", "CELL APPLICATION");
            });
            return app;
        }

        public static IServiceCollection ConfigIoc(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped<IBasedTableRepository, BasedTableRepository>();
            services.AddScoped<ISettingFieldRepository, SettingFieldRepository>();
            services.AddScoped<ISettingTableRepository, SettingTableRepository>();
            services.AddScoped<ISettingActionRepository, SettingActionRepository>();
            services.AddScoped<ISettingFormRepository, SettingFormRepository>();
            services.AddScoped<ISettingViewRepository, SettingViewRepository>();
            services.AddScoped<ISettingFieldRepository, SettingFieldRepository>();
            services.AddScoped<ISettingFieldInstanceRepository, SettingFieldInstanceRepository>();
            services.AddScoped<ISettingActionInstanceRepository, SettingActionInstanceRepository>();
            services.AddScoped<ISettingFeatureRepository, SettingFeatureRepository>();
            services.AddScoped<ISettingAdvancedRepository, SettingAdvancedRepository>();
            services.AddScoped<ISettingTreeRepository<SettingFeature>, SettingTreeRepository<SettingFeature>>();
            services.AddScoped<ISettingTreeRepository<SettingAdvanced>, SettingTreeRepository<SettingAdvanced>>();
            return services;
        }

        public static IServiceCollection ConfigValidator(this IServiceCollection service)
        {
            service.AddTransient<IValidator<SettingAction>, SettingActionValidator>();
            service.AddTransient<IValidator<SettingActionInstance>, SettingActionInstanceValidator>();
            service.AddTransient<IValidator<SettingFeature>, SettingFeatureValidator>();
            service.AddTransient<IValidator<SettingField>, SettingFieldValidator>();
            service.AddTransient<IValidator<SettingFieldInstance>, SettingFieldInstanceValidator>();
            service.AddTransient<IValidator<SettingForm>, SettingFormValidator>();
            service.AddTransient<IValidator<SettingTable>, SettingTableValidator>();
            service.AddTransient<IValidator<SettingView>, SettingViewValidator>();
            service.AddTransient<IValidator<SettingAdvanced>, SettingAdvancedValidator>();
            return service;
        }
    }
}