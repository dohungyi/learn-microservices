using Caching;
using Catalog.Application.Repositories;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class AttributeWriteOnlyRepository : BaseWriteOnlyRepository<Attribute>, IAttributeWriteOnlyRepository
{
    public AttributeWriteOnlyRepository(
        ApplicationDbContext dbContext, 
        ICurrentUser currentUser,
        ISequenceCaching sequenceCaching, 
        IServiceProvider provider
        ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
    }

    public async Task<Guid> CreateAttributeAsync(Attribute attribute, CancellationToken cancellationToken = default)
    {
        await InsertAsync(attribute, cancellationToken);
        await ClearCacheWhenChangesAsync(new List<object>() { attribute.Id }, cancellationToken);
        return attribute.Id;
    }

    public async Task UpdateAttributeAsync(Attribute attribute, CancellationToken cancellationToken)
    {
        await UpdateAsync(attribute, cancellationToken);
    }

    public async Task DeleteAttributeAsync(Attribute attribute, CancellationToken cancellationToken)
    {
        await DeleteAsync(attribute, cancellationToken);
    }
}