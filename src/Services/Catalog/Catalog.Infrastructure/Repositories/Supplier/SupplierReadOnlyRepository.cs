using AutoMapper;
using Caching;
using Catalog.Application.DTOs;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Libraries;

namespace Catalog.Infrastructure.Repositories;

public class SupplierReadOnlyRepository : BaseReadOnlyRepository<Supplier>, ISupplierReadOnlyRepository
{
    public SupplierReadOnlyRepository(
        ApplicationDbContext dbContext,
        ICurrentUser currentUser, 
        ISequenceCaching sequenceCaching,
        IServiceProvider provider
        ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
        
    }

    public async Task<IPagedList<SupplierDto>> PagingAllAsync(PagingRequest request, CancellationToken cancellationToken = default)
    {
        var mapper = _provider.GetRequiredService<IMapper>();
        
        var result = await _dbSet
            .WhereIf(!string.IsNullOrEmpty(request.SearchString),
                e => e.Name.Contains(request.SearchString) || e.Description.Contains(request.SearchString))
            .ApplySort(request.Sorts)
            .ToPagedListAsync<Supplier, SupplierDto>(
                mapper,
                request.Page,
                request.Size,
                request.IndexFrom,
                cancellationToken);

        return result;
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetByIdWithCachingAsync(id, cancellationToken);
    }

    public async Task<Supplier?> GetByAliasAsync(string alias, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultIfAsync(!string.IsNullOrEmpty(alias), e => e.Alias == alias);
    }
}