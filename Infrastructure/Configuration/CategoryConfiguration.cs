using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ParentId)
            .IsRequired();

        builder.Property(c => c.Priority)
            .IsRequired();

        // Sabit tarih değerleriyle tohumlama
        Category category1 = new()
        {
            Id = 1,
            Name = "Elektrik",
            Priority = 1,
            ParentId = 0,
            IsDeleted = false,
            CreatedDate = new DateTime(2023, 12, 31),
        };

        Category category2 = new()
        {
            Id = 2,
            Name = "Moda",
            Priority = 2,
            ParentId = 0,
            IsDeleted = false,
            CreatedDate = new DateTime(2023, 12, 31),
        };

        Category parent1 = new()
        {
            Id = 3,
            Name = "Bilgisayar",
            Priority = 1,
            ParentId = 1,
            IsDeleted = false,
            CreatedDate = new DateTime(2023, 12, 31),
        };

        Category parent2 = new()
        {
            Id = 4,
            Name = "Kadın",
            Priority = 1,
            ParentId = 2,
            IsDeleted = false,
            CreatedDate = new DateTime(2023, 12, 31),
        };

        builder.HasData(category1, category2, parent1, parent2);
    }
}