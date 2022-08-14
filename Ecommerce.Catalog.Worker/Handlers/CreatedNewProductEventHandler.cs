using EasyNetQ;
using Ecommerce.Catalog.Messaging.Models;
using Ecommerce.Catalog.Worker.Models;
using Ecommerce.Worker.Handlers;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Ecommerce.Catalog.Worker.Handlers {
    public class CreatedNewProductEventHandler : MessageHandler<CreatedNewProductEvent> {
        private readonly MongoClient mongoClient;

        public CreatedNewProductEventHandler(IBus bus, ILogger<CreatedNewProductEventHandler> logger, MongoClient mongoClient) : base(bus, logger) {
            this.mongoClient = mongoClient;
        }
        public override async Task Handle(CreatedNewProductEvent message) {
            Logger.LogInformation("Handling {object}", message);
            var db = mongoClient.GetDatabase("Products");
            var coll = db.GetCollection<Product>("Products");
            await coll.InsertOneAsync(Product.From(message));
        }
    }
}
