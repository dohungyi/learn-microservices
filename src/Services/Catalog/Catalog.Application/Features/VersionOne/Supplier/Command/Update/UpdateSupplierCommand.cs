using Catalog.Application.DTOs;
using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

// [AuthorizationRequest(new ActionExponent[] { ActionExponent.Supplier })]
[AuthorizationRequest(new ActionExponent[] { ActionExponent.AllowAnonymous })]
public class UpdateSupplierCommand : BaseUpdateCommand
{
    public UpdateSupplierDto SupplierDto { get; init; }
    public Guid SupplierId { get; set; }

    public UpdateSupplierCommand(UpdateSupplierDto supplierDto, Guid supplierId)
    {
        SupplierDto = supplierDto;
        SupplierId = supplierId;
    }
}