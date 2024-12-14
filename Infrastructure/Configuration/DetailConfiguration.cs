using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class DetailConfiguration : IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {
        builder.Property(d => d.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(d => d.Category)
            .WithMany(c => c.Details)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        var predefinedDetails = new List<Detail>
        {
            new Detail { Id = Guid.NewGuid(), Title = "Detay 1", Description = "Elektronik ürünler için açıklama", CategoryId = Guid.Parse("41111111-1111-1111-1111-111111111111") },
            new Detail { Id = Guid.NewGuid(), Title = "Detay 2", Description = "Moda kategorisi için açıklama", CategoryId = Guid.Parse("42222222-2222-2222-2222-222222222222") },
            new Detail { Id = Guid.NewGuid(), Title = "Detay 3", Description = "Telefonlar için açıklama", CategoryId = Guid.Parse("43333333-3333-3333-3333-333333333333") }
        };

        builder.HasData(predefinedDetails);
    }
}