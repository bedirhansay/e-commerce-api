namespace core.Entities;

public class Detail
{
    public Detail()
    {
        
    }

    public Detail(string title, string description, int categoryId)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
        
    }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int CategoryId { get; set; }
    public Category Category { get; set; }
}