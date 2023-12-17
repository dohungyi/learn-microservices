﻿using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence;

public class ApplicationDbContext : AppDbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    #region DbSet

    #region Tax

    public DbSet<Tax> Taxes { get; }
    
    #endregion
    
    #region Asset

    public DbSet<Asset> Assets { get; }

    #endregion

    #region Category

    public DbSet<Category> Categories { get; }

    #endregion

    #region Supplier

    public DbSet<Supplier> Suppliers { get; }

    #endregion
    
    #region Products

    public DbSet<Attribute> Attributes { get; }
    public DbSet<Product> Products { get;  }
    public DbSet<ProductAsset> ProductAssets { get; }
    public DbSet<ProductCategory> ProductCategories { get; }
    public DbSet<ProductPricing> ProductPricings{ get; }
    public DbSet<ProductReview> ProductReviews { get; }
    public DbSet<ProductSupplier> ProductSuppliers { get; }
    public DbSet<ProductVariant> ProductVariants { get; }
    public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; }
    public DbSet<ProductVariantSpecification> ProductVariantSpecifications { get; }
    
    #endregion

    #region Discount

    public DbSet<ProductDiscount> ProductDiscounts { get; }
    
    public DbSet<CategoryDiscount> CategoryDiscounts { get; }
    
    #endregion

    #region Weight

    public DbSet<Weight> Weights { get; }
    
    public DbSet<WeightCategory> WeightCategories { get; }
    
    #endregion
    
    #endregion
}