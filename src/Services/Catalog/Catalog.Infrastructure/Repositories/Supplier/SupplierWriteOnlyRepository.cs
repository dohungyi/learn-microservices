using Caching;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class SupplierWriteOnlyRepository : BaseWriteOnlyRepository<Supplier>, ISupplierWriteOnlyRepository
{
    public SupplierWriteOnlyRepository(
        ApplicationDbContext dbContext, 
        ICurrentUser currentUser, 
        ISequenceCaching sequenceCaching, 
        IServiceProvider provider
        ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
        
    }
    public async Task<Guid> DeleteSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(supplier, cancellationToken);
        
        string key = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, supplier.Alias);
        await _sequenceCaching.RemoveAsync(key, cancellationToken: cancellationToken);

        return supplier.Id;
    }
}