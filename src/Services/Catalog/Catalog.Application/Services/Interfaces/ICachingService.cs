namespace Catalog.Application.Services.Interfaces;

public interface ICachingService
{
    Task<bool> ClearAllCachingAsync(CancellationToken cancellationToken = default);
}