using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Properties;
using Catalog.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Runtime.Exceptions;

namespace Catalog.Application.Features.VersionOne;

public class GetSupplierByIdQueryHandler : BaseQueryHandler, IRequestHandler<GetSupplierByIdQuery, SupplierDto>
{
    private readonly ISupplierReadOnlyRepository _supplierReadOnlyRepository;
    private readonly IStringLocalizer<Resources> _localizer;
    
    public GetSupplierByIdQueryHandler(
        IMapper mapper,
        ISupplierReadOnlyRepository supplierReadOnlyRepository,
        IStringLocalizer<Resources> localizer
    ) : base(mapper)
    {
        _supplierReadOnlyRepository = supplierReadOnlyRepository;
        _localizer = localizer;
    }

    public async Task<SupplierDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierReadOnlyRepository.GetByIdCacheAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
        {
            throw new BadRequestException(_localizer["supplier_does_not_exist"].Value);
        }

        var supplierDto = _mapper.Map<SupplierDto>(supplier);

        return supplierDto;
    }
}