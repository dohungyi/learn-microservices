using Catalog.Application.DTOs;
using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using SharedKernel.Application;
using SharedKernel.Application.Repositories;

namespace Catalog.Application.Repositories;

public interface ISupplierReadOnlyRepository :  IEfCoreReadOnlyRepository<Supplier, IApplicationDbContext>
{
    Task<string> IsDuplicate(string code, string name, CancellationToken cancellationToken = default);
    
    Task<IPagedList<SupplierDto>> PagingAllAsync(PagingRequest request, CancellationToken cancellationToken = default);
    
    Task<Supplier?> GetByAliasWithCachingAsync(string alias, CancellationToken cancellationToken = default);
}