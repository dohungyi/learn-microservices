using Catalog.Application.DTOs;
using Catalog.Application.Properties;
using Catalog.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Runtime.Exceptions;

namespace Catalog.Application.Features.VersionOne;

public class DeleteSupplierCommandHandler : BaseCommandHandler, IRequestHandler<DeleteSupplierCommand, object>
{
    private readonly ISupplierWriteOnlyRepository _supplierWriteOnlyRepository;
    public IStringLocalizer<Resources> _localizer;
    
    public DeleteSupplierCommandHandler(
        ISupplierReadOnlyRepository supplierReadOnlyRepository,
        ISupplierWriteOnlyRepository supplierWriteOnlyRepository,
        IStringLocalizer<Resources> localizer,
        IServiceProvider provider) : base(provider)
    {
        _supplierWriteOnlyRepository = supplierWriteOnlyRepository;
        _localizer = localizer;
    }

    public async Task<object> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierWriteOnlyRepository.GetByIdAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
        {
            throw new BadRequestException(_localizer["supplier_does_not_exist"].Value);
        }

        await _supplierWriteOnlyRepository.DeleteSupplierAsync(supplier, cancellationToken);
        await _supplierWriteOnlyRepository.UnitOfWork.CommitAsync(cancellationToken);

        return request.SupplierId;
    }
}