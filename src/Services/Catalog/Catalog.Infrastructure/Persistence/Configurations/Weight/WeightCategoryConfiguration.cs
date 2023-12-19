using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class WeightCategoryConfiguration : BaseEntityConfiguration<WeightCategory>
{
    public override void Configure(EntityTypeBuilder<WeightCategory> builder)
    {
        base.Configure(builder);

        builder
            .Property(e => e.Code)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder
            .Property(e => e.Description)
            .IsRequired(false);
        
        builder
            .HasMany(e => e.ProductWeights)
            .WithOne(e => e.WeightCategory)
            .HasForeignKey(e => e.WeightCategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}