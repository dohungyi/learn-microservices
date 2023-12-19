using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class CategoryDiscountConfiguration : BaseEntityConfiguration<CategoryDiscount>
{
    public override void Configure(EntityTypeBuilder<CategoryDiscount> builder)
    {
        base.Configure(builder);
        
        builder.Property(cd => cd.DiscountUnit)
            .HasMaxLength(50);
        
        builder.Property(cd => cd.CouponCode)
            .HasMaxLength(255);

        builder.Property(cd => cd.MinimumOrderValue)
            .IsRequired();

        builder.Property(cd => cd.MaximumDiscountAmount)
            .IsRequired();

        builder.Property(cd => cd.IsRedeemAllowed)
            .IsRequired();

        builder.Property(cd => cd.CategoryId)
            .IsRequired();

    }
}