using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class ActionConfiguration : BaseEntityConfiguration<Action>
{
    public override void Configure(EntityTypeBuilder<Action> builder)
    {
        base.Configure(builder);

        #region Indexes

        // Non-Clustered Index
        builder
            .HasIndex(e => e.Name)
            .IsUnique();
        
        builder
            .HasIndex(e => e.Code)
            .IsUnique();
        
        builder
            .HasIndex(e => e.Exponent)
            .IsUnique();

        #endregion

        #region Columns
        
        builder
            .Property(a => a.Code)
            .HasMaxLength(255)
            .IsRequired()
            .IsUnicode(false);

        builder
            .Property(a => a.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(a => a.Description)
            .IsRequired(false);

        builder
            .Property(a => a.Exponent)
            .IsRequired();
        
        builder.HasMany(a => a.RoleActions)
            .WithOne(ua => ua.Action)
            .HasForeignKey(ua => ua.ActionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        #endregion
        
    }
}