using Bogus;
using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Configuration;

public class DetailConfiguration : IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {
        builder.HasKey(d => d.Id);
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


        var faker = new Faker<Detail>()
            .RuleFor(d => d.Title, f => f.Lorem.Sentence())
            .RuleFor(d => d.Description, f => f.Lorem.Sentence())
            .RuleFor(d => d.CategoryId, f => f.PickRandom<short>());

        var details = faker.Generate(3);
        builder.HasData(details);
    }
}