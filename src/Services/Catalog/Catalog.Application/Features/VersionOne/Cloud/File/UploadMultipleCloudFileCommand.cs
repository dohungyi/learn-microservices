using Catalog.Application.DTOs;
using Microsoft.AspNetCore.Http;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class UploadMultipleCloudFileCommand : BaseInsertCommand<IList<AssetDto>>
{
    public List<IFormFile> Files { get; }

    public UploadMultipleCloudFileCommand(List<IFormFile> files)
    {
        Files = files;
    }
}