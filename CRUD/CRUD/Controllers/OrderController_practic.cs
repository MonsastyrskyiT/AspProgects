using CRUD.DTO;
using CRUD.DTO_practic;
using CRUD.Services_practic;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class OrderController_practic
    {
        [ApiController]
        [Route("api/{controller}")]

        public class OrderControllerPractic : Controller
        {
            private readonly IOrderService_practic _orderService_practic;

            public OrderControllerPractic(IOrderService_practic orderService)
            {
                _orderService_practic = orderService;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                return Ok(_orderService_practic.GetAll());
            }

            [HttpGet("{id:guid}")]
            public IActionResult GetById(Guid id)
            {
                return Ok(_orderService_practic.GetById(id));
            }

            [HttpPost("create")]
            public IActionResult Create([FromBody] CreateOrderDTO_practic dto)
            {
                var newOrder = _orderService_practic.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
            }

            [HttpPut("{id:guid}")]
            public IActionResult Update(Guid id, [FromBody] UpdateOrderDTO_practic dto)
            {
                _orderService_practic.Update(id, dto);
                return NoContent();
            }

            [HttpDelete("{id:guid}")]
            public IActionResult Delete(Guid id)
            {
                _orderService_practic.Delete(id);
                return NoContent();
            }
        }
    }
}
