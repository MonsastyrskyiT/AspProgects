using Microsoft.AspNetCore.Mvc;
using ProductApi.DTO.Order;
using ProductApi.DTO.Pagination;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] OrderQueryParams queryParams)
        {
            return Ok(await _orderService.GetPagedAsync(queryParams));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto)
        {
            var order = await _orderService.CreateAsync(dto);
            return Ok(order);
        }
    }
}