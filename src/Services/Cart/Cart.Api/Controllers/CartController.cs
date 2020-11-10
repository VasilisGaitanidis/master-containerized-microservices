using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Accepted();
        }

    }
}
