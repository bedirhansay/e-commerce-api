using Bogus;
using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configuration;
public class BrandConfiguration: IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
        
        
        builder.HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(f => f.BrandId);
        
        
        var faker = new Faker<Brand>("tr") 
            .RuleFor(b => b.Id, f => Guid.NewGuid()) 
            .RuleFor(b => b.Name, f => f.Company.CompanyName()) 
            .RuleFor(b => b.IsDeleted, f => false);
        
        var fakeData = faker.Generate(3);
        builder.HasData(fakeData);
    }
    
}