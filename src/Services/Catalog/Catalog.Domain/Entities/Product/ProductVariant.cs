using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class ProductVariant : Entity
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public string TotalDescription { get; set; }
    public string Barcode { get; set; }
    public string Serial { get; set; }

    public decimal ShipPrice { get; set; }
    public decimal SOPrice { get; set; }
    public decimal POPrice { get; set; }
    public decimal WholePrice { get; set; }
    public decimal PromotionPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int StkQtyMinMin { get; set; }

    public string DfltSOUnit { get; set; }
    public string DfltPOUnit { get; set; }
    public string DfltStkUnit { get; set; }

    public string Sets { get; set; }
    public string Status { get; set; }
    public string RelatedType { get; set; }

    public string WarrantyInfo { get; set; }
    public string Type { get; set; }
    
    #region Relationships
    
    public Guid ProductId { get; set; }
    public Guid ProductAssetId { get; set; }

    #endregion
    
    #region Navigations
    
    public virtual Product Product { get; set; }
    public virtual ProductAsset ProductAsset { get; set; }
    
    public ICollection<ProductVariantAttribute> ProductVariants { get; set; }
    public ICollection<ProductVariantSpecification> ProductReviews { get; set; }
    #endregion
}