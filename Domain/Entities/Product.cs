using core.Common;

namespace core.Entities;

public class Product : EntityBase
{
    public Product(){}
    public Product(string title, string description, int price, int discount)
    {
       Title = title;
       Description = description;
       Price = price;
       Discount = discount;
       
    }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int Discount { get; set; }
    public required int BrandId { get; set; }
    public Brand Brand { get; set; }
    public ICollection<Category> Categories { get; set; }
}