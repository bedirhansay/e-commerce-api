using core.Common;

namespace core.Common;

public class EntityBase : IEntityBase
{
    public required Guid Id{ get; set;} = Guid.NewGuid();
    public DateTime CreatedDate { get; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;


}