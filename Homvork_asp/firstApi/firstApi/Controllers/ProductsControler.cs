using firstApi.Servise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsControler : Controller
    {
        private readonly IProducts _productService;

        public ProductsControler(IProducts productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product not foud" });
            }
            return Ok(product);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            var result = _productService.GetProducts()
                .Where(propa => propa.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(result);
        }
    }
}
