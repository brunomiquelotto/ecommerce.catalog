namespace Ecommerce.Catalog.Messaging.Models
{
    public record CreatedNewProductEvent
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
