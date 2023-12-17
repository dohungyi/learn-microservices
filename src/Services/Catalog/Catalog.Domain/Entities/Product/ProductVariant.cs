using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.ProductVariant)]
public class ProductVariant : Entity
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public string TotalDescription { get; set; }
    public string Barcode { get; set; }
    public string Serial { get; set; }

    public decimal SalePrice { get; set; } // Giá bán
    public decimal PurchasePrice { get; set; } // Giá nhập
    
    public int StkQtyMinMin { get; set; }
    
    public string Sets { get; set; }
    public string Status { get; set; }
    public string RelatedType { get; set; }

    public string WarrantyInfo { get; set; }
    
    #region Relationships
    
    public Guid ProductId { get; set; }
    public Guid ProductAssetId { get; set; }
    
    #endregion
    
    #region Navigations
    
    public virtual Product Product { get; set; }
    public virtual ProductAsset ProductAsset { get; set; }
    
    public ICollection<ProductPricing> ProductPricings { get; set; }
    public ICollection<ProductVariantAttribute> ProductVariantAttributes { get; set; }
    public ICollection<ProductVariantSpecification> ProductVariantSpecifications { get; set; }
    #endregion
}