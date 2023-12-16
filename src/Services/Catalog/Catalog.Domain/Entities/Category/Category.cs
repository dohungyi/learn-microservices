using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

public class Category : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public string FileName { get; set; }
    public int OrderNumber { get; set; }
    public bool Status { get; set; }
    
    #region Relationships

    public Guid? ParentId { get; set; }

    #endregion
    
    #region Navigations
    
    public virtual Category Parent { get; set; }
    public ICollection<Product>? Products { get; set; }
    
    #endregion
}