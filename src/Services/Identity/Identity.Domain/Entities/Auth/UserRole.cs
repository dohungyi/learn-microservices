using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;

[Table(TableName.UserRole)]
public class UserRole : BaseEntity
{
    #region Relationships

    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }

    #endregion
    
    #region Navigations
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
    #endregion
}
