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
        
        var predefinedCategoryIds = new List<Guid>
        {
            Guid.Parse("41111111-1111-1111-1111-111111111111"), 
            Guid.Parse("42222222-2222-2222-2222-222222222222"),
            Guid.Parse("43333333-3333-3333-3333-333333333333")
        };
        
        var faker = new Faker<Detail>()
            .RuleFor(d => d.Title, f => f.Lorem.Lines())
            .RuleFor(d => d.Description, f => f.Lorem.Sentence())
            .RuleFor(d => d.CategoryId, f => f.PickRandom(predefinedCategoryIds));

        var details = faker.Generate(3);
        builder.HasData(details);
    }
}