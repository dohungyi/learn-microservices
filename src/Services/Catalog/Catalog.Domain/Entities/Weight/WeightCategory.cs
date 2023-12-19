using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class WeightCategory : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }

    #region MyRegion
    
    public ICollection<ProductWeight> ProductWeights { get; set; }

    #endregion
}
