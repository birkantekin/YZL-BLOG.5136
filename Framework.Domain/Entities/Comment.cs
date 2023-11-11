using Framework.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Domain.Entities;

// Yorumların bulunduğu entitydir.
public class Comment : BaseEntity<long>
{
    public string Text { get; set; } // Yorum için kullanılan propertidir.

    // Hangi kullanıcının yazdığı için aşağıdaki bağlantılar yapılır.
    public  long UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public  long ArticleId { get; set; } // Correct the property name
    [ForeignKey(nameof(ArticleId))]
    public virtual Article Article { get; set; }
}