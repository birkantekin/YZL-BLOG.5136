using Framework.Domain.Common;
using Framework.Domain.Enums;

namespace Framework.Domain.Entities;

public class Article : BaseEntity<long>
{
    public Article()
    {
        Comments = new HashSet<Comment>();
        ArticleLikes = new HashSet<ArticleLike>();
        CreatedDate = DateTime.Now;
        Status = EnumType.Active;
        
    }

    public string Title { get; set; }
    public string Content { get; set; }
    public string Photo { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Duration  { get; set; }
    public int Counter { get; set; }

    public EnumType Status{ get; set; }

    //Makale ile yazarı birleştiriyoruz .(Yazar dediğimiz aslında kullanıcı)
    public  long UserId { get; set; }
    public virtual User User { get; set; }

    //Kategori ile makale bağlantısı 
    public  long CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual ICollection<Comment> Comments{ get; set; }
    public virtual ICollection<ArticleLike> ArticleLikes{ get; set; }
}
