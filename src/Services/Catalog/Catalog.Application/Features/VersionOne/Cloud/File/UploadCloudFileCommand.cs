using Catalog.Application.DTOs;
using Catalog.Application.Properties;
using Catalog.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Runtime.Exceptions;

namespace Catalog.Application.Features.VersionOne;

public class UploadCloudFileCommand : BaseInsertCommand<AssetDto>
{
    public IFormFile File { get; init; }

    public string? Description { get; init; }
    
}

public class UploadCloudFileCommandValidator : AbstractValidator<UploadCloudFileCommand>
{
    public UploadCloudFileCommandValidator(IStringLocalizer<Resources> localizer, IFileService fileService)
    {
        // Validate that the file is not null
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage(localizer["file_is_required"].Value)
            ;
        

        RuleFor(x => x.File.Length)
            .LessThan(4 * 1024 * 1024)
            .WithMessage(localizer["file_must_be_smaller_than_4_MB."].Value)
            .When(x => x.File != null);
    }
}