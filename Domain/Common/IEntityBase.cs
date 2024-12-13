namespace core.Common;

public interface IEntityBase
{
    Guid Id { get; set; }
    DateTime CreatedDate { get; } 
    DateTime? UpdatedDate { get; set; } 
    bool IsDeleted { get; set; } 
} 