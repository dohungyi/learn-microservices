using Caching;
using SharedKernel.Application;
using SharedKernel.Application.Repositories;
using SharedKernel.Auth;
using SharedKernel.Domain;
using SharedKernel.Libraries;
using SharedKernel.Persistence;
using SharedKernel.UnitOfWork;

namespace SharedKernel.Infrastructures.Repositories;

public class EfCoreWriteOnlyRepository<TEntity,TDbContext> 
    : EfCoreReadOnlyRepository<TEntity, TDbContext>, IEfCoreWriteOnlyRepository<TEntity, TDbContext>
    where TEntity : BaseEntity
    where TDbContext : AppDbContext
{
    
    public EfCoreWriteOnlyRepository(
        TDbContext dbContext, 
        ICurrentUser currentUser, 
        ISequenceCaching sequenceCaching, 
        IServiceProvider provider) 
        : base(dbContext, currentUser, sequenceCaching, provider)
    {

    }
    
    public IUnitOfWork UnitOfWork => _dbContext;

    #region [INSERTS]
    
    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        BeforeInsert(new List<TEntity>() {entity});
        
        await _dbContext.AddAsync<TEntity>(entity, cancellationToken);
        
        return entity;
    }

    public async Task<IList<TEntity>> InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        BeforeInsert(entities);
        
        await _dbContext.AddRangeAsync(entities, cancellationToken);
        
        return entities;
    }

    public async Task<IList<TEntity>> BulkInsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        BeforeInsert(entities);
        
        await _dbContext.BulkInsertEntitiesAsync(entities, cancellationToken: cancellationToken);
        
        return entities;
    }

    #endregion

    #region [UPDATE]
    
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var currentEntity = await _dbSet.FindAsync(entity.Id);
        
        BeforeUpdate(entity, currentEntity);
        
        _dbContext.Update(entity);
        
        await ClearCacheWhenChangesAsync(new List<object>() { entity.Id }, cancellationToken);
        
        return entity;
    }
    
    #endregion

    #region [DELETE]

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        BeforeDelete(new List<TEntity>() { entity });
        
        _dbContext.Remove(entity);
        
        await ClearCacheWhenChangesAsync(new List<object>() { entity.Id }, cancellationToken);
    }
    
    public async Task DeleteAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        BeforeDelete(entities);
        
        _dbContext.RemoveRange(entities);

        await ClearCacheWhenChangesAsync(entities.Select(x => (object)x.Id).ToList(), cancellationToken);
    }
    public async Task BulkDeleteAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        BeforeDelete(entities);
        
        await _dbContext.BulkDeleteEntitiesAsync(entities, cancellationToken);

        await ClearCacheWhenChangesAsync(entities.Select(x => (object)x.Id).ToList(), cancellationToken);
    }

    #endregion
    
    #region [PROTECTED]
    protected virtual void BeforeInsert(IEnumerable<TEntity> entities)
    {
        var batches = entities.ChunkList(1000);
        batches.ToList().ForEach(async entities =>
        {
            entities.ForEach(entity =>
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedBy = _currentUser.Context.OwnerId;
                entity.CreatedDate = DateHelper.Now;
                entity.LastModifiedDate = null;
                entity.LastModifiedBy = null;
                entity.DeletedDate = null;
                entity.DeletedBy = null;
            });

            if (batches.Count() > 1)
            {
                await Task.Delay(69);
            }
        });
        
    }

    protected virtual void BeforeUpdate(TEntity entity, TEntity oldValue)
    {
        entity.LastModifiedDate = DateHelper.Now;
        entity.LastModifiedBy = _currentUser.Context.OwnerId;
        entity.DeletedDate = null;
        entity.DeletedBy = null;
    }

    protected virtual void BeforeDelete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedDate = DateHelper.Now;
            entity.DeletedBy = _currentUser.Context.OwnerId;
        }
    }
    protected virtual async Task ClearCacheWhenChangesAsync(List<object> ids, CancellationToken cancellationToken = default)
    {
        var tasks = new List<Task>();
        var fullRecordKey = BaseCacheKeys.GetSystemFullRecordsKey(nameof(TEntity));
        tasks.Add(_sequenceCaching.RemoveAsync(fullRecordKey, cancellationToken: cancellationToken));

        if (ids is not null && ids.Any())
        {
            foreach (var id in ids)
            {
                var recordByIdKey = BaseCacheKeys.GetSystemRecordByIdKey(_tableName, id);
                tasks.Add(_sequenceCaching.RemoveAsync(recordByIdKey, cancellationToken: cancellationToken));
            }
        }
    }
    #endregion
    
}