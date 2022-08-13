using EasyNetQ;

namespace Ecommerce.Catalog.Worker {
    public interface IMessageHandler {
        void StartListening();
    }

    public abstract class MessageHandler<T> : IMessageHandler where T : class {
        private readonly CancellationTokenSource tokenSource;
        private readonly IBus bus;

        public MessageHandler(IBus bus, CancellationTokenSource tokenSource) {
            this.bus = bus;
            this.tokenSource = tokenSource;
        }

        protected virtual string SubscriptionId { get; } = "DefaultSubscription";

        public void StartListening() {
            Console.WriteLine($"listening to messages of type: {typeof(T)}");
            bus.PubSub.Subscribe<T>(SubscriptionId, Handle, tokenSource.Token);
        }

        public abstract Task Handle(T message);
    }

}
