using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Constants;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;

[Table(TableName.Avatar)]
public class Avatar : BaseEntity
{
    public string FileName { get; set; }
    
    #region Relationships

    public Guid UserId { get; set; }
    
    #endregion
    
    #region Navigations
    
    public virtual User User { get; set; }
    
    #endregion
}