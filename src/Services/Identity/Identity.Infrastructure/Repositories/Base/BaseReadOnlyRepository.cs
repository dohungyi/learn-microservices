namespace Catalog.Infrastructure.Repositories;

// public class BaseReadOnlyRepository<TEntity> : EfCoreReadOnlyRepository<TEntity, ApplicationDbContext>
//     where TEntity : BaseEntity
// {
//     public BaseReadOnlyRepository(
//         ApplicationDbContext dbContext, 
//         ICurrentUser currentUser, 
//         ISequenceCaching sequenceCaching, 
//         IServiceProvider provider
//     ) : base(dbContext, currentUser, sequenceCaching, provider)
//     {
//         
//     }
// }