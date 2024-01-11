using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using SharedKernel.Application.Repositories;

namespace Catalog.Application.Repositories;

public interface IAssetWriteOnlyRepository : IEfCoreWriteOnlyRepository<Asset, IApplicationDbContext>
{
    Task<Asset> CreateAssetAsync(Asset asset, CancellationToken cancellationToken = default);
    Task<IList<Asset>> CreateAssetAsync(IList<Asset> assets, CancellationToken cancellationToken = default);
    Task<Asset> UpdateAssetAsync(Asset asset, CancellationToken cancellationToken = default);
    Task<bool> DeleteAssetAsync(Asset asset, CancellationToken cancellationToken = default);
}