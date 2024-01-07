using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class LocationWardConfiguration : BaseEntityConfiguration<LocationWard>
{
    public void Configure(EntityTypeBuilder<LocationWard> builder)
    {
        
    }
}