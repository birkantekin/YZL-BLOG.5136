using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment"); // Set the table name

            builder.HasKey(x => x.Id); // Set the primary key

            builder.Property(x => x.Text).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.ArticleId).IsRequired(true);

            // Comment entity'sindeki UserId ile User entity'sini ilişkilendirme
            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
    .OnDelete(DeleteBehavior.NoAction);


            // Comment entity'sindeki ArticleId ile Article entity'sini ilişkilendirme
            builder.HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleId)
    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
