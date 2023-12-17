using Caching;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class SupplierWriteOnlyRepository : BaseWriteOnlyRepository<Supplier>
{
    public SupplierWriteOnlyRepository(
        ApplicationDbContext dbContext, 
        ICurrentUser currentUser, 
        ISequenceCaching sequenceCaching, 
        IServiceProvider provider
        ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
        
    }
}