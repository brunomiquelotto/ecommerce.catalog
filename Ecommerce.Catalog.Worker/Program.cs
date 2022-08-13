using Ecommerce.Catalog.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var configBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

var configs = configBuilder.Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<IConfiguration>(sp => configs );
serviceCollection.RegisterEasyNetQ(configs.GetConnectionString("RabbitMq"));

var cancellationToken = new CancellationTokenSource();
serviceCollection.AddSingleton<CancellationTokenSource>(cancellationToken);

serviceCollection.Scan(scan => {
    scan.FromAssemblies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.AssignableTo<IMessageHandler>())
    .AsImplementedInterfaces()
    .WithScopedLifetime();
});

serviceCollection.AddScoped<IMessageHandlerResolver, MessageHandlerResolver>(sp => 
    new MessageHandlerResolver(sp.GetServices<IMessageHandler>().ToList()));

var serviceProvider = serviceCollection.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
scope.ServiceProvider.GetRequiredService<IMessageHandlerResolver>().StartListening();

Console.CancelKeyPress += (sender, e) => {
    Console.WriteLine("Gracefully stoping...");
    cancellationToken.Cancel();
};

Console.WriteLine("Listening for messages. Hit <return> to quit.");
Console.ReadLine();