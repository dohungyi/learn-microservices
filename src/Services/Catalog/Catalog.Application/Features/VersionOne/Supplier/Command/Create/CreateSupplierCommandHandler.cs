using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Properties;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class CreateSupplierCommandHandler : BaseCommandHandler, IRequestHandler<CreateSupplierCommand, SupplierDto>
{
    private readonly ISupplierWriteOnlyRepository _supplierWriteOnlyRepository;
    private readonly IMapper _mapper;
    
    public CreateSupplierCommandHandler(
        IServiceProvider provider,
        ISupplierWriteOnlyRepository supplierWriteOnlyRepository,
        IMapper mapper
        ) : base(provider)
    {
        _supplierWriteOnlyRepository = supplierWriteOnlyRepository;
        _mapper = mapper;
    }

    public async Task<SupplierDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = _mapper.Map<Supplier>(request.CreateSupplierDto);
        
        await _supplierWriteOnlyRepository.InsertAsync(supplier, cancellationToken);
        await _supplierWriteOnlyRepository.UnitOfWork.CommitAsync(cancellationToken);

        var supplierDto = _mapper.Map<SupplierDto>(supplier);

        return supplierDto;
    }
}