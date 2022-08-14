using Ecommerce.Catalog.Messaging.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Catalog.Worker.Models {
    public class Product {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int OriginalId { get; set; }

        public static Product From(CreatedNewProductEvent message) {
            return new Product()
            {
                Name = message.Name,
                Category = message.CategoryName,
                OriginalId = message.Id
            };
        }
    }
}
