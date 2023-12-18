using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.VersionOne;

public class HomeController : BaseController
{
    // GET
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}