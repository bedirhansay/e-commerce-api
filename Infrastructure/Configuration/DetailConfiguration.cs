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

        // Sabit değerlerle tohumlama
        Detail detail1 = new()
        {
            Id = 1,
            Title = "Elektrik Detay",
            Description = "Elektrik kategorisine ait detay açıklaması.",
            CategoryId = 1,
            CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
            IsDeleted = false,
        };
        Detail detail2 = new()
        {
            Id = 2,
            Title = "Bilgisayar Detay",
            Description = "Bilgisayar kategorisine ait detay açıklaması.",
            CategoryId = 3,
            CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
            IsDeleted = true,
        };
        Detail detail3 = new()
        {
            Id = 3,
            Title = "Kadın Detay",
            Description = "Kadın kategorisine ait detay açıklaması.",
            CategoryId = 4,
            CreatedDate = DateTime.SpecifyKind(new DateTime(2023, 12, 31), DateTimeKind.Utc),
            IsDeleted = false,
        };

        builder.HasData(detail1, detail2, detail3);
    }
}