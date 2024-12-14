
using Bogus;
using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired();

        builder.HasOne(p => p.Brand)
            .WithMany(b =>b.Products)
            .HasForeignKey(f => f.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Products);


        var faker = new Faker<Product>("tr")
            .RuleFor(b => b.Id, f => Guid.NewGuid())
            .RuleFor(b => b.Title, f => f.Company.CompanyName())
            .RuleFor(b => b.Description, f => f.Company.CompanyName())
            .RuleFor(b => b.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(b => b.Discount, f => f.Random.Decimal(0, 50))
            .RuleFor(b => b.IsDeleted, f => false);
        
        var fakeData = faker.Generate(3);
        builder.HasData(fakeData);
    }
}