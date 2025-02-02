using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using SharedKernel.Domain;
using StatusType = SharedKernel.Application.Enum.StatusType;

namespace Catalog.Domain.Entities;

[Table(TableName.Supplier)]
public class Supplier : BaseEntity
{
    public string Description { get; set; }
    public string Delegate { get; set; }
    public string Bank { get; set; }
    public string AccountNumber { get; set; }
    public string BankAddress { get; set; }
    public string AddressOne { get; set; }
    public string AddressTwo { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string TaxCode { get; set; }
    public string NationCode { get; set; }
    public string ProvinceCode { get; set; }
    public string DistrictCode { get; set; }
    public bool Status { get; set; }
    
    #region Relationships

    public Guid? TaxId { get; set; }

    #endregion
    
    #region Navigations
    
    public Tax? Tax { get; set; }
    
    public ICollection<ProductSupplier>? ProductSuppliers { get; set; }
    
    #endregion
}