using core.Common;

namespace core.Common;

public class EntityBase : IEntityBase
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
      
    }
