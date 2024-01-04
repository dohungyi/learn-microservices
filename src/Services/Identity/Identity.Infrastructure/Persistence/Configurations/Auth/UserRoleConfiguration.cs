using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        
        #region Indexes

        builder
            .HasIndex(e => e.UserId);
        
        builder
            .HasIndex(e => e.RoleId);
        
        #endregion

        #region Columns

        builder
            .HasKey(ur => new { ur.UserId, ur.RoleId });
        
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
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}