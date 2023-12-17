using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain;

namespace Catalog.Infrastructure.Persistence.Configurations.Base;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(e => e.Id);
        
        builder
            .Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired(false);
    }
}