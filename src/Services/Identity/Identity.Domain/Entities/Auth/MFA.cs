using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;
using Enum = SharedKernel.Application.Enum;

namespace Identity.Domain.Entities;

[Table(TableName.MFA)]
public class MFA : Entity
{
    public Enum.MFAType Type { get; set; } = Enum.MFAType.None;

    public bool Enabled { get; set; }
    
    #region Relationships

    public Guid UserId { get; set; }
    
    #endregion
    
    #region Navigations
    
    public virtual User User { get; set; }
    
    #endregion
}