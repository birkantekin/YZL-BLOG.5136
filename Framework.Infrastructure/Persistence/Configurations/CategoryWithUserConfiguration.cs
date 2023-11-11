using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;

public class CategoryWithUserConfiguration : IEntityTypeConfiguration<CategoryWithUser>
{
    public void Configure(EntityTypeBuilder<CategoryWithUser> builder)
    {
        builder.ToTable("CategoryWithUser");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.CategoryId).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
    }
}