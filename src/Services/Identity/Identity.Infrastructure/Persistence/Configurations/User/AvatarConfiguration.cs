using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class AvatarConfiguration : BaseEntityConfiguration<Avatar>
{
    public override void Configure(EntityTypeBuilder<Avatar> builder)
    {
        base.Configure(builder);

        #region Indexes

        builder.HasIndex(e => e.UserId);
        
        #endregion

        #region Columns

        builder
            .Property(a => a.FileName)
            .HasMaxLength(255)
            .IsRequired(true);

        #endregion
    }
}