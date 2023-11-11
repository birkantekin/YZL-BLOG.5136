using Framework.Domain.Common;

namespace Framework.Domain.Entities;

public class CategoryWithUser:BaseEntity<long>
{
    public long CategoryId { get; set; }
    public long UserId { get; set; }

    public virtual Category Category { get; set; }
    public virtual User User { get; set; }

}
