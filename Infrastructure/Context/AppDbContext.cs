using System.Reflection;
using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext: DbContext
{
    public  AppDbContext (){}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  
    DbSet<Category>Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Brand> Brands { get; set; }
    DbSet<Detail> Details { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}