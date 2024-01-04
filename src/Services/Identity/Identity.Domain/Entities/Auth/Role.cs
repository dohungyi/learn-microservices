using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;

[Table(TableName.Role)]
public class Role : BaseEntity
{
    public string Code { get; set; }

    public string Name { get; set; }
    
    #region Navigations
    
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<RoleAction>? RoleActions { get; set; }
    
    #endregion
}