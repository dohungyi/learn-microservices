using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.Asset)]
public class Asset : BaseEntity
{
    public string FileName { get; set; }
    
    public string Description { get; set; }
    
    public string Path { get; set; }
    
    public SharedKernel.Application.Enum.FileType Type { get; set; }
    
    public long Size { get; set; }
    
    #region Navigations
    
    public ICollection<ProductAsset>? ProductAssets { get; set; }
    
    #endregion
    
}