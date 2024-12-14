using core.Common;

namespace core.Entities;

public class Brand: EntityBase
{
    public Brand(){}

    public Brand(string name)
    {
        Name = name;
        Products = new List<Product>();
    }
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}