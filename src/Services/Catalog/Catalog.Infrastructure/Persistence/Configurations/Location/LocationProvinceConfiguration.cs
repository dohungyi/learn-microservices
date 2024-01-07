using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Enum = System.Enum;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class LocationProvinceConfiguration : BaseEntityConfiguration<LocationProvince>
{
    public void Configure(EntityTypeBuilder<LocationProvince> builder)
    {
        base.Configure(builder);
        
        #region Indexes

        builder
            .HasIndex(e => e.Code)
            .IsUnique();

        #endregion

        #region Columns

        builder.HasMany(e => e.Districts)
            .WithOne(e => e.Province)
            .HasForeignKey(e => e.ProvinceId);
        
        builder.Property(e => e.Type)
            .HasConversion(
                v => v.ToString(),   
                v => (LocationType)Enum.Parse(typeof(LocationType), v)  
            );
        
        #endregion
    }
}