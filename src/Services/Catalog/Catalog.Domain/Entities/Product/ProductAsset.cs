using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using Catalog.Domain.Enum;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.ProductAsset)]
public class ProductAsset : BaseEntity
{
    public ProductImageType ImageType { get; set; }
    
    #region Relationships

    public Guid ProductId { get; set; }
    public Guid AssetId { get; set; }

    #endregion
    
    #region Navigations
    
    public virtual Product Product { get; set; }
    public virtual Asset Asset { get; set; }
    
    #endregion
}
