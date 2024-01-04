using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        #region Indexes

        builder
            .HasIndex(e => e.RefreshTokenValue)
            .IsUnique();

        #endregion

        #region Columns

        builder
            .Property(e => e.CurrentAccessToken)
            .IsRequired();
        
        builder
            .Property(e => e.RefreshTokenValue)
            .HasMaxLength(255)
            .IsRequired();
        
        builder
            .Property(e => e.ExpirationDate)
            .IsRequired()
            .HasColumnType("timestamp");
        

        #endregion
    }
}