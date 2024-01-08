using Caching;
using Catalog.Application.DTOs;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class AssetReadOnlyRepository : BaseReadOnlyRepository<Asset>, IAssetReadOnlyRepository
{
    public AssetReadOnlyRepository(
        ApplicationDbContext dbContext,
        ICurrentUser currentUser,
        ISequenceCaching sequenceCaching,
        IServiceProvider provider
    ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
    }

    public async Task<AssetDto?> GetAssetByIdAsync(Guid assetId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.Id == assetId)
            .Select(e => new AssetDto()
            {
                Id = e.Id,
                FileName = e.FileName,
                OriginalFileName = e.OriginalFileName,
                Url = e.Url,
                Size = e.Size,
                FileExtension = e.FileExtension,
                Description = e.Description
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<AssetDto?> GetAssetByFileNameAsync(string fileName, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.FileName.Equals(fileName))
            .Select(e => new AssetDto()
            {
                Id = e.Id,
                FileName = e.FileName,
                OriginalFileName = e.OriginalFileName,
                Url = e.Url,
                Size = e.Size,
                FileExtension = e.FileExtension,
                Description = e.Description
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IPagedList<AssetDto>> GetPagingResultAsync(PagingRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}