using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Article");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.Title).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.Content).IsRequired(true);
        builder.Property(x => x.Photo).HasMaxLength(200).IsRequired(true);
        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.Duration).IsRequired(true);
        builder.Property(x => x.Counter).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true);
        builder.Property(x => x.CategoryId).IsRequired(true);
    }
}
