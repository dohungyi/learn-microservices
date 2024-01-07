using Catalog.Application.DTOs;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;


public class GetAllWardByDistrictIdQuery : BaseAllowAnonymousQuery<IList<LocationWardDto>>
{
    public Guid DistrictId { get; init; }

    public GetAllWardByDistrictIdQuery(Guid districtId) => DistrictId = districtId;
}