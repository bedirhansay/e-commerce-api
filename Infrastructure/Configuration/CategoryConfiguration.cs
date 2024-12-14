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

        builder.HasMany(c => c.Details)
            .WithOne(d => d.Category)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Products)
            .WithMany(p => p.Categories);

        var predefinedCategories = new List<Category>
        {
            new Category { Id = Guid.Parse("41111111-1111-1111-1111-111111111111"), Name = "Elektronik", Priority = 1, ParentId = Guid.Empty },
            new Category { Id = Guid.Parse("42222222-2222-2222-2222-222222222222"), Name = "Moda", Priority = 2, ParentId = Guid.Empty },
            new Category { Id = Guid.Parse("43333333-3333-3333-3333-333333333333"), Name = "Telefonlar", Priority = 3, ParentId = Guid.Parse("41111111-1111-1111-1111-111111111111") }
        };

        builder.HasData(predefinedCategories);
    }
}