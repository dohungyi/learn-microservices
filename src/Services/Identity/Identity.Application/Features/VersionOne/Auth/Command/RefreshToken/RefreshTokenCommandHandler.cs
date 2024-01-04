using MediatR;
using SharedKernel.Application;


namespace Identity.Application.Features.VersionOne;

public class RefreshTokenCommandHandler : BaseCommandHandler, IRequestHandler<RefreshTokenCommand, ApiResult>
{
    public RefreshTokenCommandHandler(
        IServiceProvider provider
    ) : base(provider)
    {
    }

    public async Task<ApiResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return default!;
    }
}