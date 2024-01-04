using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;

[Table(TableName.RoleAction)]
public class RoleAction : BaseEntity
{
    #region Relationships

    public Guid RoleId { get; set; }
    public Guid ActionId { get; set; }

    #endregion
    
    #region Navigations
    
    public Role Role { get; set; }
    public Action Action { get; set; }
    
    #endregion
}