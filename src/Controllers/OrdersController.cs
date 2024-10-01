using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.order;
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

        // public static List<Order> orders = new List<Order>
        // {
        //     new Order
        //     {
        //         //OrderId = 1,
        //         TotalAmount = 150.50m,
        //         ShippingAddress = "123 Main St",
        //         CreatedAt = DateTime.Now.AddDays(-5),
        //     },
        //     new Order
        //     {
        //         //OrderId = 2,
        //         TotalAmount = 250.75m,
        //         ShippingAddress = "456 Elm St",
        //         CreatedAt = DateTime.Now.AddDays(-3),
        //     },
        //     new Order
        //     {
        //         //OrderId = 3,
        //         TotalAmount = 500.00m,
        //         ShippingAddress = "789 Oak St",
        //         CreatedAt = DateTime.Now.AddDays(-1),
        //     },
        // };

        // GET: api/v1/orders
        [HttpGet]
        public async Task<ActionResult<List<OrderReadDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
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

        // GET: api/v1/orders/{id}
        [HttpGet("{id}")]
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
        public async Task<ActionResult<OrderReadDto>> AddOrder(OrderCreateDto createDto)
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
        public async Task<ActionResult<bool>> UpdateOrder(Guid id, OrderUpdateDto updateDto)
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
        public async Task<ActionResult<bool>> DeleteOrder(Guid id)
        {
            var isDeleted = await _orderService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
