using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;

[Table(TableName.RefreshToken)]
public class RefreshToken : PersonalizedEntity
{
    public string RefreshTokenValue { get; set; }

    public string CurrentAccessToken { get; set; }

    public DateTime ExpirationDate { get; set; }
    
    #region Relationships

    public Guid UserId { get; set; }
    
    #endregion
    
    #region Navigations
    
    public virtual User User { get; set; }
    
    #endregion
}