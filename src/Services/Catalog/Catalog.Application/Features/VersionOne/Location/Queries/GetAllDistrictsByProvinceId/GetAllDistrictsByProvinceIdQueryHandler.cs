using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Repositories;
using MediatR;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class GetAllDistrictsByProvinceIdQueryHandler : BaseQueryHandler, IRequestHandler<GetAllDistrictsByProvinceIdQuery, IList<LocationDistrictDto>>
{
    private readonly ILocationReadOnlyRepository _locationReadOnlyRepository;
    public GetAllDistrictsByProvinceIdQueryHandler(
        IMapper mapper,
        ILocationReadOnlyRepository locationReadOnlyRepository
        ) : base(mapper)
    {
        _locationReadOnlyRepository = locationReadOnlyRepository;
    }

    public async Task<IList<LocationDistrictDto>> Handle(GetAllDistrictsByProvinceIdQuery request, CancellationToken cancellationToken)
    {
        var province = await _locationReadOnlyRepository.GetProvinceByIdAsync(request.ProvinceId, cancellationToken);
        if (province == null)
        {
            return default!;
        }
        
        return await _locationReadOnlyRepository.GetAllDistrictsByProvinceIdAsync(request.ProvinceId, cancellationToken);
    }
}