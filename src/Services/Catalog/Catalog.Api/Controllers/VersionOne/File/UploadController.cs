using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.VersionOne;

[ApiVersion("1.0")]
public class UploadController : BaseController
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync([FromForm]IFormFile file)
    {
        return Ok();
    }
    
    [HttpPost("uploads")]
    public async Task<IActionResult> UploadFileMultipleAsync([FromForm]IList<IFormFile> file)
    {
        return Ok();
    }
}