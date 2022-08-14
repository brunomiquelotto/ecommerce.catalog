using Microsoft.Extensions.Logging;

namespace Ecommerce.Worker.Handlers {
    public interface IMessageHandlerResolver {
        void StartListening(CancellationToken token);
    }
    public class MessageHandlerResolver : IMessageHandlerResolver {
        private readonly List<IMessageHandler> messageHandlers;
        private readonly ILogger<MessageHandlerResolver> logger;

        public MessageHandlerResolver(List<IMessageHandler> messageHandlers, ILogger<MessageHandlerResolver> logger) {
            this.messageHandlers = messageHandlers;
            this.logger = logger;
        }

        public void StartListening(CancellationToken token) {
            logger.LogInformation("Found {Number} handlers. Starting...", messageHandlers.Count);
            foreach (var messageHandler in messageHandlers) {
                messageHandler.StartListening(token);
            }
        }
    }
}
