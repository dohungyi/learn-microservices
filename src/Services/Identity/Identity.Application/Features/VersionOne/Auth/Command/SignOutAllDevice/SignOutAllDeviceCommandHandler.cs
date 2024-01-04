using Identity.Application.Infrastructure.Repositories;
using Identity.Application.Properties;
using MediatR;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Auth;
using SharedKernel.Runtime.Exceptions;

namespace Identity.Application.Features.VersionOne;

public class SignOutAllDeviceCommandHandler : BaseCommandHandler, IRequestHandler<SignOutAllDeviceCommand>
{
    public SignOutAllDeviceCommandHandler(
        IServiceProvider provider
    ) : base(provider)
    {
    }

    public async Task Handle(SignOutAllDeviceCommand request, CancellationToken cancellationToken)
    {
    }
}