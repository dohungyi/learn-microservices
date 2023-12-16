using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class ProductVariantSpecification : Entity
{
    public string Key { get; set; }
    public string Value { get; set; }
    
    #region Relationships

    public Guid ProductVariantId { get; set; }

    #endregion
    
    #region Navigations
    
    public virtual ProductVariant ProductVariants { get; set; }
    
    #endregion
}