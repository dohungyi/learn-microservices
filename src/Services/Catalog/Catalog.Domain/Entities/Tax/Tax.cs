using Catalog.Domain.Enum;

namespace Catalog.Domain.Entities;

public class Tax
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string TaxCateCode { get; set; }
    public decimal TaxRate { get; set; }
    public TaxType Type { get; set; }
    public bool Status { get; set; }
    
    #region Relationships
    

    #endregion
    
    #region Navigations
    
    
    #endregion
}