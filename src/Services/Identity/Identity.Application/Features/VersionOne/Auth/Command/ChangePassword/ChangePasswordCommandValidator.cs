using FluentValidation;
using Identity.Application.Properties;
using Microsoft.Extensions.Localization;

namespace Identity.Application.Features.VersionOne;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IStringLocalizer<Resources> localizer)
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage(localizer["auth_password_min_length_error"].Value);
        
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage(localizer["auth_password_min_length_error"].Value)
            .NotEqual(x => x.OldPassword)
            .WithMessage(localizer["auth_new_password_same_as_old_error"].Value);
    }
}