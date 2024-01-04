using MediatR;
using SharedKernel.Application;


namespace Identity.Application.Features.VersionOne;

public class ChangePasswordCommandHandler : BaseCommandHandler, IRequestHandler<ChangePasswordCommand, ApiResult>
{
    public ChangePasswordCommandHandler(
        IServiceProvider provider
    ) : base(provider)
    {
    }

    public async Task<ApiResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return new ApiResult();
    }
}