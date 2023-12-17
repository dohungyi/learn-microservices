using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using Catalog.Domain.Enum;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.Tax)]
public class Tax : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string TaxCateCode { get; set; }
    public decimal TaxRate { get; set; }
    public TaxType Type { get; set; }
    public bool Status { get; set; }
    
    #region Navigations

    public ICollection<Product> Products { get; set; }
    public ICollection<Supplier> Suppliers { get; set; }

    #endregion
}