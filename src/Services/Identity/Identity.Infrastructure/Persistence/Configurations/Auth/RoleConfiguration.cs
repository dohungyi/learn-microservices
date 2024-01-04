using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class RoleConfiguration : BaseEntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        #region Indexes

        builder
            .HasIndex(r => r.Code);
        
        builder
            .HasIndex(r => r.Name);
        
        #endregion

        #region Columns

        builder
            .Property(r => r.Code)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(r => r.Name)
            .HasMaxLength(255)
            .IsRequired();

        #endregion
    }
}