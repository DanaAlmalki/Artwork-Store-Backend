using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.order;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.OrderDTO;

namespace Backend_Teamwork.src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService service)
        {
            _orderService = service;
        }

        // GET: api/v1/orders
        [HttpGet]
        // [Authorize(Roles = "Admin")]  // Accessible by Admin
        public async Task<ActionResult<List<OrderReadDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET: api/v1/orders/{id}
        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin")]  // Accessible by Admin
        public async Task<ActionResult<OrderReadDto>> GetOrderById([FromRoute] Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        // POST: api/v1/orders
        [HttpPost]
        // [Authorize(Roles = "Admin")]  // Accessible by Admin
        public async Task<ActionResult<OrderReadDto>> AddOrder([FromBody] OrderCreateDto createDto)
        {
            var orderCreated = await _orderService.CreateOneAsync(createDto);
            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = orderCreated.Id },
                orderCreated
            );
        }

        // PUT: api/v1/orders/{id}
        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]  // Accessible by Admin
        public async Task<ActionResult<bool>> UpdateOrder(
            [FromRoute] Guid id,
            [FromBody] OrderUpdateDto updateDto
        )
        {
            var updateOrder = await _orderService.UpdateOneAsync(id, updateDto);
            if (!updateOrder)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }

        // DELETE: api/v1/orders/{id}
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]  // Accessible by Admin
        public async Task<ActionResult<bool>> DeleteOrder(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid user ID");
            }
            var isDeleted = await _orderService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }

        // Extra Features
        // GET: api/v1/users/page
        [HttpGet("pagination")]
        // [Authorize(Roles = "Admin")]  // Accessible by Admin

        public async Task<ActionResult<OrderReadDto>> GetOrdersByPage(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var orders = await _orderService.GetOrdersByPage(paginationOptions);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("sort-by-date")]
        public async Task<ActionResult<OrderReadDto>> SortOrdersByDate()
        {
            var orders = await _orderService.GetAllAsync();
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders.OrderBy(x => x.CreatedAt).ToList());
        }
    }
}
