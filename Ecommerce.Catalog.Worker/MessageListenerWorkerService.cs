using Ecommerce.Worker.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Catalog.Worker
{
    public class MessageListenerWorkerService : IHostedService
    {
        private readonly IMessageHandlerResolver messageHandlerResolver;
        private readonly ILogger<MessageListenerWorkerService> logger;

        public MessageListenerWorkerService(IMessageHandlerResolver messageHandlerResolver, ILogger<MessageListenerWorkerService> logger)
        {
            this.messageHandlerResolver = messageHandlerResolver;
            this.logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            messageHandlerResolver.StartListening(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stopping");
            return Task.CompletedTask;
        }
    }
}
