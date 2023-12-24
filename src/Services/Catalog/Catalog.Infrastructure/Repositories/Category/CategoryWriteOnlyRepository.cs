using Caching;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class CategoryWriteOnlyRepository : BaseWriteOnlyRepository<Category>, ICategoryWriteOnlyRepository
{
    public CategoryWriteOnlyRepository(
        ApplicationDbContext dbContext,
        ICurrentUser currentUser,
        ISequenceCaching sequenceCaching,
        IServiceProvider provider
    ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
    }

    public async Task UpdateCategoryAsync(Category supplier, CancellationToken cancellationToken = default)
    {
        string key = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, supplier.Alias);
        await Task.WhenAll(new List<Task>()
        {
            UpdateAsync(supplier, cancellationToken),
            _sequenceCaching.DeleteAsync(key, cancellationToken: cancellationToken)
        });
    }

    public async Task<Guid> DeleteCategoryAsync(Category supplier, CancellationToken cancellationToken = default)
    {
        string key = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, supplier.Alias);
        await Task.WhenAll(new List<Task>()
        {
            DeleteAsync(supplier, cancellationToken),
            _sequenceCaching.DeleteAsync(key, cancellationToken: cancellationToken)
        });

        return supplier.Id;
    }

    public async Task DeleteMultipleSupplierAsync(IList<Category> categories,
        CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(new List<Task>()
            {
                DeleteAsync(categories, cancellationToken),
            }
            .Concat(categories.Select(category =>
                _sequenceCaching.DeleteAsync(BaseCacheKeys.GetSystemRecordByIdKey(_tableName, category.Alias),
                    cancellationToken: cancellationToken))));
    }
}