using Framework.Domain.Entities;
using Framework.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.FirstName).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.LastName).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.ProfilePhoto).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.UserName).HasMaxLength(255).IsRequired(true);
        builder.Property(x => x.Password).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.RoleId).IsRequired(true);
    }
}