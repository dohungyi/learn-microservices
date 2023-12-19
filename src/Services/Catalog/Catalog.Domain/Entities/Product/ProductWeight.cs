using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class ProductWeight : Entity
{
    public decimal Value { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    
    #region Relationships
    
    public Guid ProductId { get; set; }
    public Guid WeightCategoryId { get; set; }
    
    #endregion
    
    #region Navigations
    
    public virtual Product Product { get; set; }
    public virtual WeightCategory WeightCategory { get; set; }
    
    #endregion
}