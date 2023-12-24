using AutoMapper;
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

    public async Task<CategoryHierarchyDto?> GetCategoryHierarchyByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var categoryHierarchyDto = await _dbSet
            .Where(e => e.Id == categoryId)
            .Select(e => new CategoryHierarchyDto
            {
                CurrentCategory = new CategoryDto
                {
                    Code = e.Code,
                    Name = e.Name,
                    Alias = e.Alias,
                    Description = e.Description,
                    Level = e.Level,
                    FileName = e.FileName,
                    OrderNumber = e.OrderNumber,
                    Status = e.Status,
                    Path = e.Path,
                    ParentId = e.ParentId
                },
                ChildCategories = _dbSet
                    .Where(child => child.ParentId == e.Id)
                    .Select(child => new CategoryDto
                    {
                        Code = child.Code,
                        Name = child.Name,
                        Alias = child.Alias,
                        Description = child.Description,
                        Level = child.Level,
                        FileName = child.FileName,
                        OrderNumber = child.OrderNumber,
                        Status = child.Status,
                        Path = child.Path,
                        ParentId = child.ParentId
                    })
                    .ToList(),
                AncestorCategories = e.ParentId != null
                    ? _dbSet
                        .Where(ancestor => e.Path.Substring(0, e.Path.LastIndexOf('/')).StartsWith(ancestor.Path))
                        .Select(ancestor => new CategoryDto
                        {
                            Code = ancestor.Code,
                            Name = ancestor.Name,
                            Alias = ancestor.Alias,
                            Description = ancestor.Description,
                            Level = ancestor.Level,
                            FileName = ancestor.FileName,
                            OrderNumber = ancestor.OrderNumber,
                            Status = ancestor.Status,
                            Path = ancestor.Path,
                            ParentId = ancestor.ParentId
                        })
                        .ToList()
                    : default!
            })
            .FirstOrDefaultAsync(cancellationToken);

        return categoryHierarchyDto;
    }



    public async Task<IList<Category>> GetListCategoryByIdsAsync(IList<Guid> categoryIds,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(e => categoryIds.Contains(e.Id)).ToListAsync(cancellationToken);
    }

    public async Task<string> IsDuplicate(Guid? categoryId, string code, string name,
        CancellationToken cancellationToken = default)
    {
        var duplicateSupplier = await _dbSet.FirstOrDefaultAsync(
            e => (categoryId == null || e.Id != categoryId) && (e.Code == code || e.Name == name), cancellationToken);

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
        ;
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

    public async Task<Category?> GetCategoryByAliasWithCachingAsync(string alias,
        CancellationToken cancellationToken = default)
    {
        string key = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, alias);

        var cacheResult = await _sequenceCaching.GetAsync<Category>(key, cancellationToken: cancellationToken);
        if (cacheResult is not null)
        {
            return cacheResult;
        }

        var supplier =
            await _dbSet.FirstOrDefaultIfAsync(!string.IsNullOrEmpty(alias), e => e.Alias == alias, cancellationToken);

        if (supplier is not null)
        {
            await _sequenceCaching.SetAsync(key, supplier, cancellationToken: cancellationToken);
        }

        return supplier;
    }
}