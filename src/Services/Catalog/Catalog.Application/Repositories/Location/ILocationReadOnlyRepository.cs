using Catalog.Application.DTOs;
using Catalog.Domain.Entities;

namespace Catalog.Application.Repositories;

public interface ILocationReadOnlyRepository
{
    Task<LocationProvince?> GetProvinceByIdAsync(Guid provinceId, CancellationToken cancellationToken = default);
    Task<LocationDistrict?> GetDistrictByIdAsync(Guid districtId, CancellationToken cancellationToken = default);
    Task<LocationProvince?> GetProvinceByCodeAsync(string provinceCode, CancellationToken cancellationToken = default);
    Task<LocationDistrict?> GetDistrictByCodeAsync(string districtCode, CancellationToken cancellationToken = default);
    Task<IList<LocationProvinceDto>> GetAllProvincesAsync(CancellationToken cancellationToken = default);
    Task<IList<LocationDistrictDto>> GetAllDistrictsByProvinceIdAsync(Guid provinceId, CancellationToken cancellationToken = default);
    Task<IList<LocationWardDto>> GetAllWardsByDistrictIdAsync(Guid districtId, CancellationToken cancellationToken = default);
    Task<IList<LocationDistrictDto>> GetAllDistrictsByProvinceCodeAsync(string provinceCode, CancellationToken cancellationToken = default);
    Task<IList<LocationWardDto>> GetAllWardsByDistrictCodeAsync(string districtCode, CancellationToken cancellationToken = default);
}