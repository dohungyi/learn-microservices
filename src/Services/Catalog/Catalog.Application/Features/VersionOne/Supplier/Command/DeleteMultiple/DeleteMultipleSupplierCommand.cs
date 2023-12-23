using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

// [AuthorizationRequest(new ActionExponent[] { ActionExponent.Supplier })]
[AuthorizationRequest(new ActionExponent[] { ActionExponent.AllowAnonymous })]
public class DeleteMultipleSupplierCommand : BaseDeleteCommand<IList<Guid>>
{
    public IList<Guid> SupplierIds { get; init; }

    public DeleteMultipleSupplierCommand(IList<Guid> supplierIds) => SupplierIds = supplierIds;
    
}