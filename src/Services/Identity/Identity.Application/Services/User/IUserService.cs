namespace Identity.Application.Infrastructure.Services;

public interface IUserService
{
    Task<string> GetAvatarUrlByFileNameAsync(string fileName, object userId, CancellationToken cancellationToken);
}