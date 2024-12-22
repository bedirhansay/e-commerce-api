using Bogus;
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
        
        Faker faker = new("tr");

        Product product1 = new()
        {
            Id = 1,
            Title = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            BrandId = 1,
            Discount = faker.Random.Decimal(0, 10),
            Price = faker.Finance.Amount(10, 1000),
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
        };
        Product product2 = new()
        {
            Id = 2,
            Title = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            BrandId = 3,
            Discount = faker.Random.Decimal(0, 10),
            Price = faker.Finance.Amount(10, 1000),
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
        };

        builder.HasData(product1, product2);
    }
}