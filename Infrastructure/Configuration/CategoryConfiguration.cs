using Bogus;
using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
 public void Configure(EntityTypeBuilder<Category> builder)
 {
  builder.HasKey(c => c.Id);
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
  
  var predefinedCategoryIds = new List<Guid>
  {
   Guid.Parse("41111111-1111-1111-1111-111111111111"), 
   Guid.Parse("42222222-2222-2222-2222-222222222222"),
   Guid.Parse("43333333-3333-3333-3333-333333333333")
  };



  var faker = new Faker<Category>()
   .RuleFor(c => c.Id, c => c.Random.Guid())
   .RuleFor(c => c.Name, c => c.Name.FirstName())
   .RuleFor(c => c.Priority, c => c.Random.Int())
   .RuleFor(c => c.ParentId, c => c.PickRandom(predefinedCategoryIds));
  
  var products = faker.Generate(5);
  builder.HasData(products);
 }
}
