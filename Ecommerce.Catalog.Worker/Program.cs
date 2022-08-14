using Ecommerce.Worker.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
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
        serviceCollection.AddMessageHandlers();
        serviceCollection.AddHostedServices();

        serviceCollection.AddScoped<MongoClient>(sp => {
            return new MongoClient(context.Configuration.GetConnectionString("MongoDb"));
        });
    })
    .Build();

await host.RunAsync();