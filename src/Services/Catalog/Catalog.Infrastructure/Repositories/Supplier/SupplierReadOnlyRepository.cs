using Caching;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class SupplierReadOnlyRepository : BaseReadOnlyRepository<Supplier>
{
    public SupplierReadOnlyRepository(
        ApplicationDbContext dbContext,
        ICurrentUser currentUser, 
        ISequenceCaching sequenceCaching,
        IServiceProvider provider
        ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
    }
}