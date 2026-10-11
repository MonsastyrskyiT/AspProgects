using CRUD.DTO;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/shop-product")]   
    [ApiExplorerSettings(GroupName = "shop")]
    public class ShopProductControlers_practic03_10_2026 : Controller
    {
        private readonly IProductService _productService;

        public ShopProductControlers_practic03_10_2026(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>Get product by id</summary>
        /// <param name="id">Product id</param>
        /// <returns>Product</returns>
        /// <response code="200">Product found</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            return Ok(_productService.GetById(id));
        }

        /// <summary>Create new product</summary>
        /// <param name="dto">Data for creating product</param>
        /// <returns>Created product</returns>
        /// <response code="201">Product created</response>
        /// <response code="400">Invalid data</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateProductDTO dto)
        {
            var newProduct = _productService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        /// <summary>Update existing product</summary>
        /// <param name="id">Product id</param>
        /// <param name="dto">Updated data</param>
        /// <response code="204">Product updated</response>
        /// <response code="404">Product not found</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, [FromBody] UpdatePriductDTO dto)
        {
            _productService.Update(id, dto);
            return NoContent();
        }

        /// <summary>Delete product</summary>
        /// <param name="id">Product id</param>
        /// <response code="204">Product deleted</response>
        /// <response code="404">Product not found</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            _productService.Delete(id);
            return NoContent();
        }
    }
}