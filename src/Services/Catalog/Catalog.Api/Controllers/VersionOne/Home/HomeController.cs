using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.VersionOne;

public class HomeController : BaseController
{
    // GET
    [HttpGet("index"), AllowAnonymous]
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}