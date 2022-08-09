using EasyNetQ;
using Ecommerce.Catalog.Messaging.Models;

namespace Ecommerce.Catalog.Messaging {
    public interface ICatalogClient {
        Task Publish(CreatedNewProductEvent productEvent);
    }

    public class CatalogClient : ICatalogClient {
        public CatalogClient() {

        }
        public async Task Publish(CreatedNewProductEvent productEvent) {
            using var bus = RabbitHutch.CreateBus("host=rabbitmq");
            await bus.PubSub.PublishAsync(productEvent);
        }
    }
}
