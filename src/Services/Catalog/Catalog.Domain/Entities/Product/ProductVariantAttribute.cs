using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class ProductVariantAttribute : Entity
{
    
    #region Relationships

    public Guid ProductVariantId { get; set; }
    public Guid AttributeId { get; set; }

    #endregion
    
    #region Navigations
    
    public virtual ProductVariant ProductVariants { get; set; }
    public virtual Attribute Attribute { get; set; }
    
    #endregion
}