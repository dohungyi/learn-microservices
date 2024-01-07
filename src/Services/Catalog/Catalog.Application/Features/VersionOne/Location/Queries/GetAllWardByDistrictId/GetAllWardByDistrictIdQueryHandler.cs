using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Repositories;
using MediatR;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class GetAllWardByDistrictIdQueryHandler : BaseQueryHandler, IRequestHandler<GetAllWardByDistrictIdQuery, IList<LocationWardDto>>
{
    private readonly ILocationReadOnlyRepository _locationReadOnlyRepository;
    public GetAllWardByDistrictIdQueryHandler(
        IMapper mapper,
        ILocationReadOnlyRepository locationReadOnlyRepository
    ) : base(mapper)
    {
        _locationReadOnlyRepository = locationReadOnlyRepository;
    }

    public async Task<IList<LocationWardDto>> Handle(GetAllWardByDistrictIdQuery request, CancellationToken cancellationToken)
    {
        var district = await _locationReadOnlyRepository.GetDistrictByIdAsync(request.DistrictId, cancellationToken);
        if (district == null)
        {
            return default!;
        }
        
        return await _locationReadOnlyRepository.GetAllWardsByDistrictIdAsync(request.DistrictId, cancellationToken);
    }
}