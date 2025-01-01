using Bogus;
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

        builder.HasData(
            new Brand
            {
                Id = 1,
                Name = "Elektronik",
                CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
                IsDeleted = false
            },
            new Brand
            {
                Id = 2,
                Name = "Mobilya",
                CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
                IsDeleted = false
            },
            new Brand
            {
                Id = 3,
                Name = "Kozmetik",
                CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
                IsDeleted = true
            }
        );

    }
}