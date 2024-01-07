using Catalog.Application.DTOs;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;


public class GetAllWardsByDistrictIdQuery : BaseAllowAnonymousQuery<IList<LocationWardDto>>
{
    public Guid DistrictId { get; init; }

    public GetAllWardsByDistrictIdQuery(Guid districtId) => DistrictId = districtId;
}