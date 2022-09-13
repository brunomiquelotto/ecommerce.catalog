using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Catalog.Data.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(x => x.Category).WithMany(x => x.Products);
        builder.HasKey(x => x.Id);
        builder.ToTable("Products");
    }
}