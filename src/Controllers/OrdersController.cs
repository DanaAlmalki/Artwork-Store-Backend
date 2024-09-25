using Microsoft.AspNetCore.Mvc;

namespace Backend_Teamwork.src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public static List<Order> orders = new List<Order>
        {
            new Order
            {
                OrderId = 1,
                TotalAmount = 150.50m,
                ShippingAddress = "123 Main St",
                CreatedAt = DateTime.Now.AddDays(-5),
            },
            new Order
            {
                OrderId = 2,
                TotalAmount = 250.75m,
                ShippingAddress = "456 Elm St",
                CreatedAt = DateTime.Now.AddDays(-3),
            },
            new Order
            {
                OrderId = 3,
                TotalAmount = 500.00m,
                ShippingAddress = "789 Oak St",
                CreatedAt = DateTime.Now.AddDays(-1),
            },
        };

        // GET: api/v1/orders
        [HttpGet]
        public ActionResult GetOrders()
        {
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("sort-by-date")]
        public ActionResult SortOrdersByDate()
        {
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders.OrderBy(x => x.CreatedAt).ToList());
        }

        // GET: api/v1/orders/{id}
        [HttpGet("{id}")]
        public ActionResult GetOrderById(int id)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        // POST: api/v1/orders
        [HttpPost]
        public ActionResult AddOrder(Order newOrder)
        {
            orders.Add(newOrder);
            return Created("", "Order created successfully");
        }

        // PUT: api/v1/orders/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            order.TotalAmount = updatedOrder.TotalAmount;
            order.ShippingAddress = updatedOrder.ShippingAddress;
            order.CreatedAt = updatedOrder.CreatedAt;

            return NoContent();
        }

        // DELETE: api/v1/orders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            orders.Remove(order);
            return NoContent();
        }
    }
}
