using EasyNetQ;
using Ecommerce.Catalog.Messaging.Models;
using Ecommerce.Worker.Handlers;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Catalog.Worker.Handlers {
    public class CreatedNewProductEventHandler : MessageHandler<CreatedNewProductEvent> {
        public CreatedNewProductEventHandler(IBus bus, ILogger<CreatedNewProductEventHandler> logger) : base(bus, logger) {

        }
        public override Task Handle(CreatedNewProductEvent message) {
            Logger.LogInformation("Handling {object}", message);
            return Task.CompletedTask;
        }
    }
}
