using Microsoft.AspNetCore.Mvc;
using SvaggerOpenIP.DTO.SwaggerOpenApi.DTO;
using SvaggerOpenIP.Services;

namespace SvaggerOpenIP.Controllers
{
    [ApiController]
    [Route("aip/[controller]")]
    [Produces("application/json")]// controller returns json
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
//<summary>Create new product</summary>
        ///<param name="dto">Data for create product</param>
        ///<returns>Created product</returns>
        ///<response code = "201">Product created</response>
        ///<response code = "400">Invalid data</response>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ProductDTO productDTO)
        {
            var product = _productService.Create(productDTO);
            return Ok(product);
            //return CreatedAtAction("Created: ", new { product = product });
        }
    }
}
