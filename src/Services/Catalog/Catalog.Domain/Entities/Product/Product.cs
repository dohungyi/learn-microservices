using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.Product)]
public class Product : BaseEntity
{
    public string TaxCateCode { get; set; }
    
    public bool HomeFlag { get; set; }
    public bool HotFlag { get; set; }
    public bool IsBestSelling { get; set; }
    public bool IsNew { get; set; }
    public bool IsHot { get; set; }
    public int ViewCount { get; set; }
    
    #region Relationships

    public Guid SupplierId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid TaxId { get; set; }
    public Guid ProductPricingId { get; set; }

    #endregion
    
    #region Navigations
    
    public virtual Supplier Supplier { get; set; }
    public virtual Category Category { get; set; }
    public virtual Tax Tax { get; set; }
    public virtual ProductPricing ProductPricing { get; set; }
    
    public ICollection<ProductVariant> ProductVariants { get; set; }
    public ICollection<ProductReview> ProductReviews { get; set; }
    public ICollection<ProductAsset> ProductAssets { get; set; }
    public ICollection<ProductDiscount> Discounts { get; set; }
    #endregion
}