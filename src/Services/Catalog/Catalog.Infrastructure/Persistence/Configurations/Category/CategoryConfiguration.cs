using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
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
            .Property(e => e.Status)
            .HasDefaultValue(true);
        
        builder
            .Property(e => e.ParentId)
            .IsRequired(false);

        builder
            .HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);

    }
}