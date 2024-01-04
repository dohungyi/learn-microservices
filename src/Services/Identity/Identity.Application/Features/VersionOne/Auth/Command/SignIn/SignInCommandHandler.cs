using MediatR;
using SharedKernel.Application;

namespace Identity.Application.Features.VersionOne;

public class SignInCommandHandler : BaseCommandHandler, IRequestHandler<SignInCommand, ApiResult>
{
    public SignInCommandHandler(
        IServiceProvider provider
    ) : base(provider)
    {
    }

    public async Task<ApiResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        return default!;
    }
}