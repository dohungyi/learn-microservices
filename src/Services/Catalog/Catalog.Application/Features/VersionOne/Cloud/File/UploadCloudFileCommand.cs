using Catalog.Application.DTOs;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class UploadCloudFileCommand : BaseInsertCommand<AssetDto>
{
    public IFormFile File { get; init; }

    public UploadCloudFileCommand(IFormFile formFile) => File = formFile;
}