using CRUD.DTO;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{

    [ApiController]
    [Route("api/{controller}")]
    [ApiExplorerSettings(GroupName = "shop")]//
    public class ProductController : Controller
    {
        private readonly IProductService _productController;

        public ProductController(IProductService productService)
        {
            _productController = productService;
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_productController.GetById(id));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateProductDTO dto)
        {
            var newProduct = _productController.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdatePriductDTO dto)
        {
            _productController.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _productController.Delete(id);
            return NoContent();
        }


    }
}
