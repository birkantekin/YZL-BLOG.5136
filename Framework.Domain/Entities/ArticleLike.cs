using Framework.Domain.Common;

namespace Framework.Domain.Entities;

// Kullanıcının beğendiği makale beğenileri entititsi.
public class ArticleLike:BaseEntity<long>
{
    public bool IsLike { get; set; }// Beğeni yapıldığındaki durum ve kaldırma işlemi için tutulan değer.

    public long ArticleId { get; set; }
    public virtual Article Article { get; set; }
    //Kullanıcı birleştirme işlemi
    public  long UserId { get; set; }
    public virtual User User { get; set; }
}
