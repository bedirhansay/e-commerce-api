using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Price)
            .IsRequired();

        // Sabit değerlerle tohumlama
        Product product1 = new()
        {
            Id = 1,
            Title = "Telefon",
            Description = "Yüksek performanslı bir akıllı telefon.",
            BrandId = 1,
            Discount = 5.5m,
            Price = 850.99m,
            CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
            IsDeleted = false,
        };
        Product product2 = new()
        {
            Id = 2,
            Title = "Laptop",
            Description = "Yüksek çözünürlüklü ekran ve güçlü performans.",
            BrandId = 3,
            Discount = 7.2m,
            Price = 1200.75m,
            CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
            IsDeleted = false,
        };

        builder.HasData(product1, product2);
    }
}