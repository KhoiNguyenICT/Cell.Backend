using Microsoft.Extensions.DependencyInjection;

namespace Cell.Core.RestClient
{
    public static class AddRestClientExtension
    {
        public static IServiceCollection AddRestClient(this IServiceCollection services)
        {
            services.AddTransient<IRestClient, RestClient>();
            return services;
        }
    }
}
