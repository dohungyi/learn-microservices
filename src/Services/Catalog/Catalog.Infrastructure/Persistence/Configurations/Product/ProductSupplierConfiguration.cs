using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;



public class ProductSupplierConfiguration : EntityConfiguration<ProductSupplier>
{
    public override void Configure(EntityTypeBuilder<ProductSupplier> builder)
    {
        base.Configure(builder);
        
        #region Indexes

        

        #endregion

        #region Columns

        builder
            .HasKey(sc => new { sc.ProductId, sc.SupplierId });

        builder
            .HasOne(e => e.Product)
            .WithMany(e => e.ProductSuppliers)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(e => e.Supplier)
            .WithMany(e => e.ProductSuppliers)
            .HasForeignKey(e => e.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}