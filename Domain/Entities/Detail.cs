using core.Common;

namespace core.Entities;

public class Detail : EntityBase
{
    public Detail() { }

    public Detail(string title, string description, Guid categoryId)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
    }

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Guid CategoryId { get; set; } 
    public Category Category { get; set; } 
}