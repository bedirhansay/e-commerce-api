using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
        
        var predefinedBrands = new List<Brand>
        {
            new Brand { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Brand A", IsDeleted = false },
            new Brand { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Brand B", IsDeleted = false },
            new Brand { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Brand C", IsDeleted = false }
        };

        builder.HasData(predefinedBrands);
    }
}