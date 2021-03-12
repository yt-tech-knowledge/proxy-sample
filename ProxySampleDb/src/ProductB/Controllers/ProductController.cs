using Microsoft.AspNetCore.Mvc;

namespace ProductB.Controllers
{
    [ApiController]
    [Route("v2/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            return Ok(product);
        }
    }
}
