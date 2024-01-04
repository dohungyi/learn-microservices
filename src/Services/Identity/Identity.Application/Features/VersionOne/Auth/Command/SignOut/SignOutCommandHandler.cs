using Identity.Application.Infrastructure.Repositories;
using MediatR;
using SharedKernel.Application;
using SharedKernel.Auth;

namespace Identity.Application.Features.VersionOne;

public class SignOutCommandHandler : BaseCommandHandler, IRequestHandler<SignOutCommand, Unit>
{
    public SignOutCommandHandler(
        IServiceProvider provider
    ) : base(provider)
    {
    }

    public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}