using Framework.Domain.Common;

namespace Framework.Domain.Entities;

//Kullanıcının rollerinin bulunduğu entity'dir.
public class Role : BaseEntity<long>
{
    public string Name { get; set; }// Rol Adı 
}
