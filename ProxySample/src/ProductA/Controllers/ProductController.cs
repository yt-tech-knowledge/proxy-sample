using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductA.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string[] _products = new[]
        {
            "Rice", "Banana", "Coffe", "Orange"
        };

        [HttpGet]
        public IActionResult Get()
        {
            var products = _products.Select(item => new Product
            {
                Name = item
            }).ToList();

            return Ok(products);
        }
    }
}
