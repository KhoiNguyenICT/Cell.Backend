using System;
using System.Reflection;
using AutoMapper;
using Cell.Application.Api.Mappers;
using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.Filters;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.DynamicEntity;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SecuritySessionEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Entities.SettingActionInstanceEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Entities.SettingFeatureEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;
using Cell.Model.Entities.SettingFilterEntity;
using Cell.Model.Entities.SettingFormEntity;
using Cell.Model.Entities.SettingReportEntity;
using Cell.Model.Entities.SettingTableEntity;
using Cell.Model.Entities.SettingViewEntity;
using Cell.Service.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Cell.Application.Api.Helpers
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

        public static IServiceCollection ConfigIdentity(this IServiceCollection service)
        {
            service.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            return service;
        }

        public static IServiceCollection ConfigSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v5", new Info { Title = "CELL APIs", Version = "5.0.0" });
                c.OperationFilter<SwaggerHeaderFilter>();
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

        public static IServiceCollection ConfigIoc(this IServiceCollection service)
        {
            service.AddScoped<AppDbContextSeed>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddScoped<IBasedTableService, BasedTableService>();
            service.AddScoped<ISecurityGroupService, SecurityGroupService>();
            service.AddScoped<ISecurityPermissionService, SecurityPermissionService>();
            service.AddScoped<ISecuritySessionService, SecuritySessionService>();
            service.AddScoped<ISecurityUserService, SecurityUserService>();
            service.AddScoped<ISettingActionService, SettingActionService>();
            service.AddScoped<ISettingActionInstanceService, SettingActionInstanceService>();
            service.AddScoped<ISettingAdvancedService, SettingAdvancedService>();
            service.AddScoped<ISettingFeatureService, SettingFeatureService>();
            service.AddScoped<ISettingFieldService, SettingFieldService>();
            service.AddScoped<ISettingFieldInstanceService, SettingFieldInstanceService>();
            service.AddScoped<ISettingFilterService, SettingFilterService>();
            service.AddScoped<ISettingFormService, SettingFormService>();
            service.AddScoped<ISettingReportService, SettingReportService>();
            service.AddScoped<ISettingTableService, SettingTableService>();
            service.AddScoped<ISettingViewService, SettingViewService>();
            service.AddScoped<IDynamicService, DynamicService>();
            service.AddScoped<ISettingTreeService<SettingAdvanced>, SettingTreeService<SettingAdvanced>>();
            service.AddScoped<ISettingTreeService<SecurityGroup>, SettingTreeService<SecurityGroup>>();
            return service;
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
            service.AddTransient<IValidator<SettingFilter>, SettingFilterValidator>();
            service.AddTransient<IValidator<SettingReport>, SettingReportValidator>();
            service.AddTransient<IValidator<SecurityGroup>, SecurityGroupValidator>();
            service.AddTransient<IValidator<SecurityUser>, SecurityUserValidator>();
            service.AddTransient<IValidator<SecurityPermission>, SecurityPermissionValidator>();
            return service;
        }
    }
}