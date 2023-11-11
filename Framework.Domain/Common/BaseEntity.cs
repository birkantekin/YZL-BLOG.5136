namespace Framework.Domain.Common;

// Entiti'ler için base sınıf oluştuk
public abstract class BaseEntity<T>
{
    // Id sütununu tanımladık , tipi dinamik olarak gelecektir.
    public T Id { get; set; }
}
