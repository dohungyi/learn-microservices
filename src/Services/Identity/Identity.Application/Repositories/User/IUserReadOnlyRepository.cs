using Identity.Application.Persistence;
using Identity.Domain.Entities;
using SharedKernel.Application.Repositories;

namespace Identity.Application.Infrastructure.Repositories;

public interface IUserReadOnlyRepository : IEfCoreReadOnlyRepository<User, IApplicationDbContext>
{
    Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<string> CheckDuplicateAsync(string username, string email, string phone,
        CancellationToken cancellationToken = default);

    Task<Avatar> GetAvatarAsync(CancellationToken cancellationToken = default);

    Task<User> GetUserInformationAsync(CancellationToken cancellationToken = default);

    Task<User> GetUserInformationByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}