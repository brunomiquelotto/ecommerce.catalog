using Ecommerce.Worker.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Ecommerce.Worker.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddMessageHandlers(this IServiceCollection serviceCollection) {
            serviceCollection.Scan(scan => {
                scan.FromAssemblies(Assembly.GetEntryAssembly()!)
                .AddClasses(classes => classes.AssignableTo<IMessageHandler>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            serviceCollection.AddScoped<IMessageHandlerResolver, MessageHandlerResolver>(sp =>
                new MessageHandlerResolver(sp.GetServices<IMessageHandler>().ToList(), sp.GetRequiredService<ILogger<MessageHandlerResolver>>()));
            return serviceCollection;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection serviceCollection) {
            serviceCollection.Scan(scan => {
                scan.FromAssemblies(Assembly.GetEntryAssembly()!)
                .AddClasses(classes => classes.AssignableTo<IHostedService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            return serviceCollection;
        }
    }
}
