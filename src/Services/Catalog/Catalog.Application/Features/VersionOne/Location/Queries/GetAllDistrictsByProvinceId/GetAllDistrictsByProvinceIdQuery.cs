using Catalog.Application.DTOs;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class GetAllDistrictsByProvinceIdQuery : BaseAllowAnonymousQuery<IList<LocationDistrictDto>>
{
    public Guid ProvinceId { get; init; }

    public GetAllDistrictsByProvinceIdQuery(Guid provinceId) => ProvinceId = provinceId;
}