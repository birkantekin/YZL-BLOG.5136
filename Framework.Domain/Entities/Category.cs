using Framework.Domain.Common;
using Framework.Domain.Enums;

namespace Framework.Domain.Entities;

//Makale kategorilerinin entitysidir.
public class Category : BaseEntity<long>
{
    public Category()
    {
        Articles = new List<Article>();
        CategoryWithUsers = new List<CategoryWithUser>();
    }
    public required string Name { get; set; }// Kategorinin adı

    public EnumType Status { get; set; }

    public virtual List<Article> Articles { get; set; }
    public virtual List<CategoryWithUser> CategoryWithUsers { get; set; }
}
