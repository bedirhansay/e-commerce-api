using core.Common;

namespace core.Entities;

public class Category : EntityBase
{
    public Category() { }

    public Category(string name, Guid parentId, int priority)
    {
        Name = name;
        ParentId = parentId;
        Priority = priority;
    }

    public Guid ParentId { get; set; } 
    public required string Name { get; set; }
    public int Priority { get; set; } = 0;

    public ICollection<Detail> Details { get; set; } = new List<Detail>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}