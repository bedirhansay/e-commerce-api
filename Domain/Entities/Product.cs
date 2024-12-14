using core.Common;

namespace core.Entities;

public class Product : EntityBase
{
    public Product() { }

    public Product(string title, string description, decimal price, int discount, Guid brandId)
    {
        Title = title;
        Description = description;
        Price = price;
        Discount = discount;
        BrandId = brandId;
       
    }

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int Discount { get; set; }
    public Guid BrandId { get; set; } 

    public Brand Brand { get; set; } 
    public ICollection<Category> Categories { get; set; } = new List<Category>(); // Kategoriler
}