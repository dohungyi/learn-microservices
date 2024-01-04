using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class UserConfigConfiguration : BaseEntityConfiguration<UserConfig>
{
    public override void Configure(EntityTypeBuilder<UserConfig> builder)
    {
        base.Configure(builder);
        
        #region Indexes

        builder.HasIndex(e => e.UserId);

        #endregion

        #region Columns

        builder
            .Property(e => e.Json)
            .IsRequired();
        
        #endregion
    }
}