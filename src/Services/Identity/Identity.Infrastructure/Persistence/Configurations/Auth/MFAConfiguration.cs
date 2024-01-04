using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class MFAConfiguration : EntityConfiguration<MFA>
{
    public override void Configure(EntityTypeBuilder<MFA> builder)
    {
        base.Configure(builder);
        
    }
}