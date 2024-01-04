using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.InMySql.Persistence.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        #region Indexes

        builder
            .HasIndex(e => e.Id);
        
        builder
            .HasIndex(e => e.Username);
        
        builder
            .HasIndex(e => e.Email);

        #endregion

        #region Columns

        builder
            .Property(u => u.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(u => u.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(u => u.Salt)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(u => u.PhoneNumber)
            .HasMaxLength(20)
            .IsUnicode(false);

        builder
            .Property(u => u.Email)
            .HasMaxLength(255)
            .IsUnicode(false);

        builder
            .Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder
            .Property(u => u.LastName)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(u => u.Address)
            .HasMaxLength(255)
            .IsUnicode(false);
        
        // Reference Property
        builder
            .HasOne(u => u.Avatar)
            .WithOne(a => a.User)
            .HasForeignKey<Avatar>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        
        builder
            .HasOne(u => u.UserConfig)
            .WithOne(uc => uc.User)
            .HasForeignKey<UserConfig>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder
            .HasOne(u => u.RefreshToken)
            .WithOne(uc => uc.User)
            .HasForeignKey<RefreshToken>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        
        builder
            .HasMany(u => u.SignInHistories)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        
        #endregion
    }
}