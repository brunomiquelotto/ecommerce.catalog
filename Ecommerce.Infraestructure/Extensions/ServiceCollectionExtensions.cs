using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Ecommerce.Infraestructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdGeneratorClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IIdGeneratorClient>().ConfigureHttpClient(options =>
            {
                options.BaseAddress = new Uri(configuration["IIdGeneratorClientUrl"].ToString());
            });

            return services;
        }
    }
}
