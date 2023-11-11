using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");// Tablomuzun adını belirttik

        builder.HasKey(x => x.Id);// Tablomuzun primary key alanını belirledik.

        // Tablonun veritabanındaki özellikleri belirttik.
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired(true);
    }
}