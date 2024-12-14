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


  var faker = new Faker<Category>()
   .RuleFor(c => c.Id, c => c.Random.Guid())
   .RuleFor(c => c.Name, c => c.Name.FirstName())
   .RuleFor(c => c.Priority, c => c.Random.Int())
   .RuleFor(c => c.ParentId, c => c.Random.Int());
  
  var products = faker.Generate(5);
  builder.HasData(products);
 }
}
