using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;

// ArticleLike tablomuzun ayarlarını yaptık.
public class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLike>
{
    public void Configure(EntityTypeBuilder<ArticleLike> builder)
    {
        builder.ToTable("ArticleLike");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.IsLike).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.ArticleId).IsRequired(true);

        builder.HasOne(c => c.Article)
            .WithMany(a => a.ArticleLikes)
            .HasForeignKey(c => c.ArticleId)
.OnDelete(DeleteBehavior.NoAction);
    }
}