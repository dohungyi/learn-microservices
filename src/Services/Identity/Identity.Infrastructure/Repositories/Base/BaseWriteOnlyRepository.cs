namespace Catalog.Infrastructure.Repositories;

// public class BaseWriteOnlyRepository<TEntity> : EfCoreWriteOnlyRepository<TEntity, ApplicationDbContext>
//     where TEntity : BaseEntity
// {
//     public BaseWriteOnlyRepository(
//         ApplicationDbContext dbContext, 
//         ICurrentUser currentUser, 
//         ISequenceCaching sequenceCaching, 
//         IServiceProvider provider
//         ) : base(dbContext, currentUser, sequenceCaching, provider)
//     {
//         
//     }
//     
// }