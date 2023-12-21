using Catalog.Application.DTOs;
using Catalog.Application.Persistence;
using Catalog.Domain.Entities;
using SharedKernel.Application;
using SharedKernel.Application.Repositories;

namespace Catalog.Application.Repositories;

public interface ISupplierReadOnlyRepository :  IEfCoreReadOnlyRepository<Supplier, IApplicationDbContext>
{
    Task<IPagedList<SupplierDto>> PagingAllAsync(PagingRequest request, CancellationToken cancellationToken = default);

    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Supplier?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default);
}