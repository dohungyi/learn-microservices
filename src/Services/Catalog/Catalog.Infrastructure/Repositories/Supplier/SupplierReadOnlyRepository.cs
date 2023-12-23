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

    public async Task<string> IsDuplicate(string code, string name, CancellationToken cancellationToken = default)
    {
        var duplicateSupplier = await _dbSet.FirstOrDefaultAsync(e => e.Code == code || e.Name == name, cancellationToken);

        if (duplicateSupplier?.Code == code)
        {
            return "supplier_is_duplicate_code";
        }
        
        if (duplicateSupplier?.Name == code)
        {
            return "supplier_is_duplicate_name";
        }
        
        return "";
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

    public async Task<Supplier?> GetByAliasWithCachingAsync(string alias, CancellationToken cancellationToken = default)
    {
        string key = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, alias);
        
        var cacheResult = await _sequenceCaching.GetAsync<Supplier>(key, cancellationToken: cancellationToken);
        if (cacheResult is not null)
        {
            return cacheResult;
        }
        
        var supplier = await _dbSet.FirstOrDefaultIfAsync(!string.IsNullOrEmpty(alias), e => e.Alias == alias, cancellationToken);

        if (supplier is not null)
        {
            await _sequenceCaching.SetAsync(key, supplier, cancellationToken: cancellationToken);
        }

        return supplier;
    }
}