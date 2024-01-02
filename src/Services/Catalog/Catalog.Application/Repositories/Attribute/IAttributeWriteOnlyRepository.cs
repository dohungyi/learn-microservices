using Catalog.Application.DTOs;
using Catalog.Application.Persistence;
using SharedKernel.Application.Repositories;

namespace Catalog.Application.Repositories;

public interface IAttributeWriteOnlyRepository : IEfCoreWriteOnlyRepository<Attribute, IApplicationDbContext>
{
    Task<Guid> CreateAttributeAsync(Attribute attribute, CancellationToken cancellationToken = default);

    Task UpdateAttributeAsync(Attribute attribute, CancellationToken cancellationToken = default);

    Task DeleteAttributeAsync(Attribute attribute, CancellationToken cancellationToken = default);
}