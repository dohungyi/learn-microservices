using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class AssetConfiguration : BaseEntityConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        builder
            .Property(e => e.FileName)
            .HasMaxLength(255);

        builder
            .Property(e => e.Description)
            .IsRequired(false);

        builder
            .Property(e => e.Path)
            .IsRequired(false);
    }
    
}