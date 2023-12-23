using AutoMapper;
using Catalog.Application.Properties;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Runtime.Exceptions;

namespace Catalog.Application.Features.VersionOne;

public class UpdateSupplierCommandHandler : BaseCommandHandler, IRequestHandler<UpdateSupplierCommand, Unit>
{
    private readonly ISupplierWriteOnlyRepository _supplierWriteOnlyRepository;
    private readonly IStringLocalizer<Resources> _localizer;
    private readonly IMapper _mapper;
    public UpdateSupplierCommandHandler(
        ISupplierWriteOnlyRepository supplierWriteOnlyRepository,
        IStringLocalizer<Resources> localizer,
        IMapper mapper,
        IServiceProvider provider
        ) : base(provider)
    {
        _supplierWriteOnlyRepository = supplierWriteOnlyRepository;
        _localizer = localizer;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierWriteOnlyRepository.GetByIdAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
        {
            throw new BadRequestException(_localizer["supplier_does_not_exist"].Value);
        }
        
        supplier = _mapper.Map(request.SupplierDto, supplier);
        supplier.Id = request.SupplierId;
        
        await _supplierWriteOnlyRepository.UpdateSupplierAsync(supplier, cancellationToken);
        await _supplierWriteOnlyRepository.UnitOfWork.CommitAsync(cancellationToken);
        
        return Unit.Value;
    }
}