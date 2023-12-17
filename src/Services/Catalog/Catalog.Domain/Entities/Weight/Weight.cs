using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class Weight : Entity
{
    public decimal Value { get; set; }
    
    #region Navigations
    
    public virtual Product Product { get; set; }
    public virtual WeightCategory WeightCategory { get; set; }
    #endregion
}