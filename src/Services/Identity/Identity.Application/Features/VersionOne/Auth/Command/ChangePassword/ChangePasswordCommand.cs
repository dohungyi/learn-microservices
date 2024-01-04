using SharedKernel.Application;

namespace Identity.Application.Features.VersionOne;

public class ChangePasswordCommand : BaseUpdateCommand<ApiResult>
{
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}