using System.Data;
using System.Data.SqlClient;
using Cell.Helpers.Interfaces;
using Cell.Helpers.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cell.Helpers
{
    public static class StartupHelpers
    {
        public static IServiceCollection ConfigHelpers(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<ISearchProvider, SqlSearchProvider>();
            service.AddScoped<IWriteProvider, SqlWriteProvider>();
            service.AddScoped<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            return service;
        }
    }
}