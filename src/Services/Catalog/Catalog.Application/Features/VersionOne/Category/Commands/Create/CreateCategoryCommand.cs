using Catalog.Application.DTOs;
using Catalog.Application.Mappings;
using Catalog.Domain.Entities;
using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

[AuthorizationRequest(new ActionExponent[] { ActionExponent.Category })]
public class CreateCategoryCommand : BaseInsertCommand<CategoryDto>, IMapFrom<Category>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Alias  { get; set; }
    public string Description { get; set; }
    public int? Level { get; set; }
    public string FileName { get; set; }
    public int OrderNumber { get; set; }
    public bool Status { get; set; }
    public Guid? ParentId { get; set; }
}