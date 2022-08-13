using EasyNetQ;
using Ecommerce.Catalog.Messaging.Models;
using Newtonsoft.Json;

namespace Ecommerce.Catalog.Worker.Handlers {
    public class CreatedNewProductEventHandler : MessageHandler<CreatedNewProductEvent> {
        public CreatedNewProductEventHandler(IBus bus, CancellationTokenSource src) : base(bus, src) {

        }
        public override Task Handle(CreatedNewProductEvent message) {
            Console.WriteLine(JsonConvert.SerializeObject(message));
            return Task.CompletedTask;
        }
    }
}
