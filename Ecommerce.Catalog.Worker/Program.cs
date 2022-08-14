using Ecommerce.Worker.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

using var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(config => {
        config
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
    })
    .ConfigureServices((context, serviceCollection) => {
        serviceCollection.RegisterEasyNetQ(context.Configuration.GetConnectionString("RabbitMq"));

        serviceCollection.Scan(scan => {
            scan.FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo<IMessageHandler>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<IHostedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();
        });

        serviceCollection.AddScoped<IMessageHandlerResolver, MessageHandlerResolver>(sp =>
            new MessageHandlerResolver(sp.GetServices<IMessageHandler>().ToList(), sp.GetRequiredService<ILogger<MessageHandlerResolver>>()));
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Worker service started.");