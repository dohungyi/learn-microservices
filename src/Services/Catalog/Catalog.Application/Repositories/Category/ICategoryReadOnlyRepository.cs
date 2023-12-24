using Catalog.Application.DTOs;
using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using SharedKernel.Application;
using SharedKernel.Application.Repositories;

namespace Catalog.Application.Repositories;


public interface ICategoryReadOnlyRepository :  IEfCoreReadOnlyRepository<Category, IApplicationDbContext>
{
    Task<CategoryHierarchyDto?> GetCategoryHierarchyByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    
    Task<IList<Category>> GetListCategoryByIdsAsync(IList<Guid> categoryIds, CancellationToken cancellationToken = default);
    
    Task<string> IsDuplicate(Guid? categoryId, string code, string name, CancellationToken cancellationToken = default);
    
    Task<IPagedList<CategoryDto>> GetPagingResultAsync(PagingRequest request, CancellationToken cancellationToken = default);
    
    Task<Category?> GetCategoryByAliasWithCachingAsync(string alias, CancellationToken cancellationToken = default);
}