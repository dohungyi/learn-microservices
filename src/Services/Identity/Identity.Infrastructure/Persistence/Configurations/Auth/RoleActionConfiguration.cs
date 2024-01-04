using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class RoleActionConfiguration : IEntityTypeConfiguration<RoleAction>
{
    public void Configure(EntityTypeBuilder<RoleAction> builder)
    {
        
        #region Indexes

        builder
            .HasIndex(e => e.ActionId);
        
        builder
            .HasIndex(e => e.RoleId);
        
        #endregion

        #region Columns
        
        builder
            .HasKey(ur => new { ur.ActionId, ur.RoleId });
        
        builder
            .Property(e => e.CreatedBy)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(e => e.CreatedDate)
            .IsRequired();
        
        builder
            .Property(e => e.LastModifiedBy)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
            .Property(e => e.LastModifiedDate)
            .IsRequired(false);
        
        builder
            .Property(e => e.DeletedBy)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder
            .Property(e => e.DeletedDate)
            .IsRequired(false);
        
        builder
            .HasOne(ur => ur.Role)
            .WithMany(u => u.RoleActions)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ur => ur.Action)
            .WithMany(r => r.RoleActions)
            .HasForeignKey(ur => ur.ActionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        #endregion
    }
}