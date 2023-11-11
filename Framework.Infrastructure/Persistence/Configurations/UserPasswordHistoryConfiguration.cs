using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;

public class UserPasswordHistoryConfiguration : IEntityTypeConfiguration<UserPasswordHistory>
{
    public void Configure(EntityTypeBuilder<UserPasswordHistory> builder)
    {
        builder.ToTable("UserPasswordHistory");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.Password).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
    }
}