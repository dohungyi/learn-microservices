using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class OTPConfiguration : EntityConfiguration<OTP>
{
    public override void Configure(EntityTypeBuilder<OTP> builder)
    {
        base.Configure(builder);
    }
}