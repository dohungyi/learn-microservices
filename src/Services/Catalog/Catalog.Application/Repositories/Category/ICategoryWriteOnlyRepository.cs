using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using SharedKernel.Application.Repositories;

namespace Catalog.Application.Repositories;


public interface ICategoryWriteOnlyRepository : IEfCoreWriteOnlyRepository<Category, IApplicationDbContext>
{
    Task UpdateCategoryAsync(Category supplier, CancellationToken cancellationToken = default);
    
    Task<Guid> DeleteCategoryAsync(Category supplier, CancellationToken cancellationToken = default);

    Task DeleteMultipleSupplierAsync(IList<Category> categories, CancellationToken cancellationToken = default);
    
}