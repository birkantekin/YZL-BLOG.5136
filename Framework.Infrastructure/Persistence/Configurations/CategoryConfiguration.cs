using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;


// Kategori tablosunun ayarlamalarını yaptık.
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true);
    }
}