using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

// [AuthorizationRequest(new ActionExponent[] { ActionExponent.Supplier })]
[AuthorizationRequest(new ActionExponent[] { ActionExponent.AllowAnonymous })]
public class DeleteSupplierCommand : BaseDeleteCommand<object>
{
    public Guid SupplierId { get; set; }

    public DeleteSupplierCommand(Guid supplierId) => SupplierId = supplierId;
}