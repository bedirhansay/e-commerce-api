namespace core.Common;

public interface IEntityBase
{
    int Id { get; set; }
    DateTime CreatedDate { get; } 
    bool IsDeleted { get; set; } 
} 