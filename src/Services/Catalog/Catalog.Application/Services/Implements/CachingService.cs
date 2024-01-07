using Caching;
using Catalog.Application.Services.Interfaces;

namespace Catalog.Application.Services.Implements;

public class CachingService : ICachingService
{
    private readonly ISequenceCaching _caching;
    public CachingService(ISequenceCaching caching)
    {
        _caching = caching;
    }
    public async Task<bool> ClearAllCachingAsync(CancellationToken cancellationToken = default)
    {
        await _caching.DeleteByPatternAsync("*", cancellationToken: cancellationToken);
        return true;
    }
}