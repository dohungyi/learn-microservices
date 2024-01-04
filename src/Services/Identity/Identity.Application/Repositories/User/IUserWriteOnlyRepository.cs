using Identity.Application.Persistence;
using Identity.Domain.Entities;
using SharedKernel.Application.Repositories;

namespace Identity.Application.Infrastructure.Repositories;

public interface IUserWriteOnlyRepository : IEfCoreWriteOnlyRepository<User, IApplicationDbContext>
{
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);

    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default);

    Task<User> DeleteUserAsync(User user, CancellationToken cancellationToken = default);

    Task SetAvatarAsync(string fileName, CancellationToken cancellationToken = default);

    Task RemoveAvatarAsync(CancellationToken cancellationToken = default);
}