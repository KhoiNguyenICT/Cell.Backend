using System.Data;
using System.Data.SqlClient;
using Cell.Helpers.Interfaces;
using Cell.Helpers.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Cell.Helpers
{
    public static class StartupHelpers
    {
        public static IServiceCollection ConfigHelpers(this IServiceCollection service)
        {
            service.AddScoped<ISearchProvider, SqlSearchProvider>();
            service.AddScoped<IWriteProvider, SqlWriteProvider>();
            service.AddScoped<IDbConnection>(db => new SqlConnection("DefaultConnection"));
            return service;
        }
    }
}