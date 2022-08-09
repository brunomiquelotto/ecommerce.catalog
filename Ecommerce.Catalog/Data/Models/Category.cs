using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Catalog.Data.Models;

public class Category {
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
    public void Configure(EntityTypeBuilder<Category> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.ToTable("Categories");
        builder.HasMany(x => x.Products).WithOne(x => x.Category);
    }
}