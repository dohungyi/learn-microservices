using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.VersionOne;

[ApiVersion("1.0")]
public class UploadController : BaseController
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm]IFormFile file)
    {
        return Ok();
    }
}