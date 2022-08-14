using EasyNetQ;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ecommerce.Worker.Handlers {
    public interface IMessageHandler {
        void StartListening(CancellationToken token);
    }

    public abstract class MessageHandler<T> : IMessageHandler where T : class {
        protected readonly ILogger<MessageHandler<T>> Logger;
        private readonly IBus bus;

        public MessageHandler(IBus bus, ILogger<MessageHandler<T>> logger) {
            this.bus = bus;
            this.Logger = logger;
        }

        protected virtual string SubscriptionId { get; } = "DefaultSubscription";

        public void StartListening(CancellationToken token) {
            Logger.LogInformation("Listening to '{Type}' with SubscriptionId: {SubscriptionId}", typeof(T), SubscriptionId);
            bus.PubSub.Subscribe<T>(SubscriptionId, TryHandle, token);
        }

        public async Task TryHandle(T message) {
            using var scope = Logger.BeginScope("{Handler}", typeof(MessageHandler<T>));
            Stopwatch timeToHandle = new();
            try {
                timeToHandle.Start();
                await this.Handle(message);
            }
            catch (Exception ex) {
                Logger.LogError("Exception caught trying to handle {message}", message);
                Logger.LogError("Exception: {ex}", ex);
                throw;
            } finally {
                timeToHandle.Stop();
                Logger.LogInformation("Time to handle: {time}ms", timeToHandle.ElapsedMilliseconds);
            }
        }

        public abstract Task Handle(T message);
    }

}
