using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;

namespace Identity.Domain.Entities;

[Table(TableName.User)]
public class User : SharedKernel.Domain.User
{
    #region Navigations
    
    public virtual Avatar? Avatar { get; set; }
    
    public virtual UserConfig? UserConfig { get; set; }
    
    public virtual RefreshToken? RefreshToken { get; set; }
    
    public ICollection<UserRole>? UserRoles { get; set; }
    
    public ICollection<SignInHistory>? SignInHistories { get; set; }
    
    #endregion
}