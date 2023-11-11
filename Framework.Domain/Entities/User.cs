using Framework.Domain.Common;
using Framework.Domain.Enums;

namespace Framework.Domain.Entities;

public class User : BaseEntity<long>//Id tipimiz long oldu
{
    public User()
    {
        Comments = new HashSet<Comment>();
    }
    public string FirstName { get; set; }// Ad
    public string LastName { get; set; }// Soyad
    public string ProfilePhoto { get; set; }// string olarak tutulur veritabanında
    public string Email { get; set; }// E-posta
    public string UserName { get; set; }// Kullanıcı Adı
    public string Password { get; set; }// Şifre alanırdır
    public EnumType IsActive { get; set; } // Kullanıcının aktif ve pasif durumu
    public long RoleId { get; set; }// Kullanıcı ve rol tablosunu bire çok ilişkiyle birleştirdik.
    public virtual Role? Role { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

}