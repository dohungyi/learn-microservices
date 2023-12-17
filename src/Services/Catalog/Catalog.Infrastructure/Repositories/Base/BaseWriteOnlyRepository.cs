using Caching;
using Catalog.Infrastructure.Persistence;
using SharedKernel.Auth;
using SharedKernel.Domain;
using SharedKernel.Infrastructures.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class BaseWriteOnlyRepository<TEntity> : EfCoreWriteOnlyRepository<TEntity, ApplicationDbContext>
    where TEntity : BaseEntity
{
    public BaseWriteOnlyRepository(
        ApplicationDbContext dbContext, 
        ICurrentUser currentUser, 
        ISequenceCaching sequenceCaching, 
        IServiceProvider provider
        ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
        
    }
    
}