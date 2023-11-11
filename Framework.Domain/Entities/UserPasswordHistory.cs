using Framework.Domain.Common;

namespace Framework.Domain.Entities;

// Kullanıcının eski şifreleri için oluşturulan sınıfdır.
public class UserPasswordHistory : BaseEntity<long>
{
    public string Password { get; set; }// Kullanıcının eski şifrelerinin tutulduğu propertidir.
    public DateTime CreatedDate { get; set; }

    // Kullanıcıyı bağlama işlemidir.
    public long UserId { get; set; }

    public virtual User User { get; set; }
}