using Microsoft.AspNetCore.Mvc;
using SvaggerOpenIP.Services;

namespace SvaggerOpenIP.DTO
{
    [ApiController]
    [Route("ap/[controller]")]
    [Produces("application/json")]// controller returns json
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    }
}
