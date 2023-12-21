using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence;

public class ApplicationDbContext : AppDbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    #region DbSet
    
    #region Asset

    public DbSet<Asset> Assets { get; set; }

    #endregion

    #region Category

    public DbSet<Category> Categories { get; set; }

    #endregion

    #region Supplier

    public DbSet<Supplier> Suppliers { get; set; }

    #endregion
    
    #region Products

    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<Product> Products { get;  }
    public DbSet<ProductAsset> ProductAssets { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductPricing> ProductPricings{ get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }
    public DbSet<ProductVariantSpecification> ProductVariantSpecifications { get; set; }
    public DbSet<ProductWeight> ProductWeights { get; set; }
    
    #endregion

    #region Discount

    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    
    public DbSet<CategoryDiscount> CategoryDiscounts { get; set; }
    
    #endregion

    #region Weight
    
    public DbSet<Weight> Weights { get; set; }
    
    #endregion
    
    #endregion
    
}