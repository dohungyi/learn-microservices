using Catalog.Application.Mappings;
using Catalog.Domain.Entities;
using MediatR;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class UpdateCategoryCommand : BaseUpdateCommand<Unit>, IMapFrom<Category>
{
    public Guid Id { get; set; }
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