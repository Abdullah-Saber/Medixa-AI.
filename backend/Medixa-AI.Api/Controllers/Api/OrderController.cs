using Microsoft.AspNetCore.Mvc;
using Medixa_AI.Application.DTOs;

namespace Medixa_AI.Api.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        // TODO: Inject IOrderService via constructor
        // private readonly IOrderService _orderService;

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            // var orders = await _orderService.GetAllOrdersAsync();
            // return Ok(orders);
            return Ok(new List<OrderDto>());
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            // var order = await _orderService.GetOrderByIdAsync(id);
            // if (order == null) return NotFound();
            // return Ok(order);
            return Ok(new OrderDto());
        }

        // GET: api/order/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetPatientOrders(Guid patientId)
        {
            // var orders = await _orderService.GetPatientOrdersAsync(patientId);
            // return Ok(orders);
            return Ok(new List<OrderDto>());
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            // var order = await _orderService.CreateOrderAsync(dto);
            // return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            return Ok(new OrderDto());
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto dto)
        {
            // await _orderService.UpdateOrderAsync(id, dto);
            // return NoContent();
            return NoContent();
        }
    }
}
