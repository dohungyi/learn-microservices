using Catalog.Application.DTOs;
using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

[AuthorizationRequest(new ActionExponent[] { ActionExponent.Supplier })]
public class CreateSupplierCommand : BaseInsertCommand<SupplierDto>
{
    public CreateSupplierDto CreateSupplierDto { get; init; }

    public CreateSupplierCommand(CreateSupplierDto createSupplierDto)
    {
        CreateSupplierDto = createSupplierDto;
    }
}