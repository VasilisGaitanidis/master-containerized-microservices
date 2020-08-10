using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
