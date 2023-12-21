using Catalog.Application.DTOs;
using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

// [AuthorizationRequest(new ActionExponent[] { ActionExponent.Supplier })]
[AuthorizationRequest(new ActionExponent[] { ActionExponent.AllowAnonymous })]
public class GetSupplierByIdQuery : BaseQuery<SupplierDto>
{
    public Guid SupplierId { get; init; }

    public GetSupplierByIdQuery(Guid supplierId) => SupplierId = supplierId;
}