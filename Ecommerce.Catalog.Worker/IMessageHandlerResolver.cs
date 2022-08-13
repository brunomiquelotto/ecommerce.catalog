namespace Ecommerce.Catalog.Worker {
    public interface IMessageHandlerResolver {
        void StartListening();
    }
    public class MessageHandlerResolver : IMessageHandlerResolver {
        private readonly List<IMessageHandler> messageHandlers;

        public MessageHandlerResolver(List<IMessageHandler> messageHandlers) {
            this.messageHandlers = messageHandlers;
        }

        public void StartListening() {
            foreach (var messageHandler in messageHandlers) {
                messageHandler.StartListening();
            }
        }
    }
}
