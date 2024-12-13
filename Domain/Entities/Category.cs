using core.Common;

namespace core.Entities;

public class Category:EntityBase
{
    public Category(){}
    public Category(string name, int parentId, int priority)
    {
        Name = name;
        ParentId = parentId;
        Priority = priority;
    }

    
    public required string Name { get; set; }
    public required int ParentId { get; set; } 
    public required int Priority { get; set; }
    public ICollection<Detail> Details { get; set; }
    public ICollection<Product> Products { get; set; }
}