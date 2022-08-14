﻿using EasyNetQ;
using Microsoft.Extensions.Logging;

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
            bus.PubSub.Subscribe<T>(SubscriptionId, Handle, token);
        }

        public abstract Task Handle(T message);
    }

}
