using core.Common;

namespace core.Entities;

public class ProductCategory : IEntityBase
{
    public int Id { get; set; } 
    public DateTime CreatedDate { get; set; } 
    public bool IsDeleted { get; set; } 

    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product Product { get; set; }
    public Category Category { get; set; }
}