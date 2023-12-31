using AutoMapper;
using AutoMapper.QueryableExtensions;
using Caching;
using Catalog.Application.DTOs;
using Catalog.Application.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Linq;
using SharedKernel.Libraries;

namespace Catalog.Infrastructure.Repositories;

public class CategoryReadOnlyRepository : BaseReadOnlyRepository<Category>, ICategoryReadOnlyRepository
{
    public CategoryReadOnlyRepository(
        ApplicationDbContext dbContext,
        ICurrentUser currentUser,
        ISequenceCaching sequenceCaching,
        IServiceProvider provider
    ) : base(dbContext, currentUser, sequenceCaching, provider)
    {
    }

    public async Task<IList<CategorySummaryDto>> GetCategoryHierarchyByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return default;
    }
    
    public async Task<IList<Category>> GetListCategoryByIdsAsync(IList<Guid> categoryIds,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(e => categoryIds.Contains(e.Id) && e.Status).ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryByIdAsync(object categoryId, CancellationToken cancellationToken = default)
    {
        return await GetByIdWithCachingAsync(categoryId, cancellationToken);
    }

    public async Task<string> IsDuplicate(Guid? categoryId, string code, string name,
        CancellationToken cancellationToken = default)
    {
        var duplicateSupplier = await _dbSet.FirstOrDefaultAsync(
            e => (categoryId == null || e.Id != categoryId) && (e.Code == code || e.Name == name) && e.Status, cancellationToken);

        if (duplicateSupplier is null)
        {
            return string.Empty;
        }

        if (duplicateSupplier.Code == code)
        {
            return "supplier_is_duplicate_code";
        }

        if (duplicateSupplier.Name == name)
        {
            return "supplier_is_duplicate_name";
        }

        return string.Empty;
    }

    public async Task<bool> IsParentCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.ParentId == categoryId && e.Status, cancellationToken);
    }

    public async Task<bool> HasProductCategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProductCategories.AnyAsync(e => e.CategoryId == categoryId, cancellationToken);
    }

    public async Task<IPagedList<CategoryDto>> GetPagingResultAsync(PagingRequest request,
        CancellationToken cancellationToken = default)
    {
        var mapper = _provider.GetRequiredService<IMapper>();

        var result = await _dbSet
            .WhereIf(!string.IsNullOrEmpty(request.SearchString),
                e => e.Name.Contains(request.SearchString) || e.Description.Contains(request.SearchString))
            .ApplySort(request.Sorts)
            .ToPagedListAsync<Category, CategoryDto>(
                mapper,
                request.Page,
                request.Size,
                request.IndexFrom,
                cancellationToken);

        return result;
    }

    public async Task<Category?> GetCategoryByAliasAsync(string alias, CancellationToken cancellationToken = default)
    {
        string key = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, alias);

        var cacheResult = await _sequenceCaching.GetAsync<Category>(key, cancellationToken: cancellationToken);
        if (cacheResult is not null)
        {
            return cacheResult;
        }

        var supplier =
            await _dbSet.FirstOrDefaultIfAsync(!string.IsNullOrEmpty(alias), e => e.Alias == alias && e.Status, cancellationToken);

        if (supplier is not null)
        {
            await _sequenceCaching.SetAsync(key, supplier, cancellationToken: cancellationToken);
        }

        return supplier;
    }

    public async Task<IList<CategoryNavigationDto>> GetCategoryNavigationAsync(CancellationToken cancellationToken = default)
    {
        string key = BaseCacheKeys.GetSystemFullRecordsKey(_tableName);
        var categoryNavigationDtos = await _sequenceCaching.GetAsync<IList<CategoryNavigationDto>>(key, CachingType.Redis, cancellationToken: cancellationToken);

        if (categoryNavigationDtos != null)
        {
            return categoryNavigationDtos;
        }
        
        var roots = _dbSet.Where(x => !x.ParentId.HasValue && x.Status)
            .OrderBy(x => x.OrderNumber);
        
        var hasRoots = await roots.AnyAsync(cancellationToken);
        if(hasRoots)
        {
            return default!;
        }

        categoryNavigationDtos = await roots.Select(root => new CategoryNavigationDto
            {
               Children = _dbSet.Where(x => x.ParentId.Equals(root.Id) && x.Status)
                   .OrderBy(x => x.OrderNumber)
                   .Select(child => new CategoryNavigationDto
                   {
                       Id = child.Id,
                       ParentId = child.ParentId,
                       Name = child.Name,
                       Alias = child.Alias,
                       Description = child.Description,
                       Level = child.Level,
                       FileName = child.FileName,
                       OrderNumber = child.OrderNumber,
                       Status = child.Status,
                       Path = child.Path,
                       Children = BuildCategoryChildren(child.Id, cancellationToken)
                   })
                   .ToList()
            })
            .ToListAsync(cancellationToken);
        
        await _sequenceCaching.SetAsync(key, categoryNavigationDtos, cancellationToken: cancellationToken);
        
        return categoryNavigationDtos;
    }
    
    private IList<CategoryNavigationDto> BuildCategoryChildren(Guid parentId, CancellationToken cancellationToken)
    {
        var children = _dbSet.Where(x => x.ParentId.Equals(parentId) && x.Status)
            .OrderBy(x => x.OrderNumber)
            .Select(child => new CategoryNavigationDto
            {
                Id = child.Id,
                ParentId = child.ParentId,
                Name = child.Name,
                Alias = child.Alias,
                Description = child.Description,
                Level = child.Level,
                FileName = child.FileName,
                OrderNumber = child.OrderNumber,
                Status = child.Status,
                Path = child.Path,
                Children = BuildCategoryChildren(child.Id, cancellationToken)
            })
            .ToList();

        return children;
    }
}