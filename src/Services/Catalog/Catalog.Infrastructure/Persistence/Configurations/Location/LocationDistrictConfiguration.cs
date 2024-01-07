using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Enum = System.Enum;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class LocationDistrictConfiguration : BaseEntityConfiguration<LocationDistrict>
{
    public void Configure(EntityTypeBuilder<LocationDistrict> builder)
    {
        base.Configure(builder);
        
        #region Indexes

        builder
            .HasIndex(e => e.Code)
            .IsUnique();

        #endregion

        #region Columns

        builder.HasMany(e => e.Wards)
            .WithOne(e => e.District)
            .HasForeignKey(e => e.DistrictId);
        
        builder.Property(e => e.Type)
            .HasConversion(
                v => v.ToString(),   
                v => (LocationType)Enum.Parse(typeof(LocationType), v)  
            );
        
        #endregion
    }
}