using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;


[Table(TableName.Action)]
public class Action : BaseEntity
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }
    
    public int Exponent { get; set; }
    
    #region Navigations
    
    public ICollection<RoleAction>? RoleActions { get; set; }
    
    #endregion
}