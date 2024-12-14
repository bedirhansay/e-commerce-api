
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
        
        var predefinedBrandIds = new List<Guid>
        {
            Guid.Parse("11111111-1111-1111-1111-111111111111"), 
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Guid.Parse("33333333-3333-3333-3333-333333333333")  
        };
        
        var predefinedCategories = new List<Category>
        {
            new Category { Id = Guid.Parse("41111111-1111-1111-1111-111111111111"), Name = "Elektronik", Priority = 1, ParentId = Guid.Parse("42222222-2222-2222-2222-222222222222") },
            new Category { Id = Guid.Parse("42222222-2222-2222-2222-222222222222"), Name = "Moda", Priority = 2,  ParentId = Guid.Parse("42222222-2222-2222-2222-222222222222")},
            new Category { Id = Guid.Parse("43333333-3333-3333-3333-333333333333"), Name = "Telefonlar", Priority = 3, ParentId = Guid.Parse("41111111-1111-1111-1111-111111111111") }
        };



        var faker = new Faker<Product>("tr")
            .RuleFor(b => b.Id, f => Guid.NewGuid())
            .RuleFor(b => b.Title, f => f.Company.CompanyName())
            .RuleFor(b => b.Description, f => f.Company.CompanyName())
            .RuleFor(b => b.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(b => b.Discount, f => f.Random.Decimal(0, 50))
            .RuleFor(b => b.IsDeleted, f => false)
            .RuleFor(p => p.BrandId, f => f.PickRandom(predefinedBrandIds))
            .RuleFor(p => p.Categories, f => f.PickRandom(predefinedCategories, f.Random.Int(1, 2)).ToList());
        
        var fakeData = faker.Generate(3);
        builder.HasData(fakeData);
    }
}