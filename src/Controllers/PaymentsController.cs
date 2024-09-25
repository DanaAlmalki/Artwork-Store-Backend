using Backend_Teamwork.src.Entities;
using Microsoft.AspNetCore.Mvc;
using Backend_Teamwork.src.Entities;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        public static List<Payment> payments = new List<Payment>()
        {
            new Payment
            {
                Id = 1,
                PaymentMethod = "Credit Card ",
                Amount = 100.0m,
                CreatedAt = new DateTime(2024, 9, 20),
            },
            new Payment
            {
                Id = 2,
                PaymentMethod = "PayPal",
                Amount = 250.0m,
                CreatedAt = new DateTime(2024, 9, 12),
            },
            new Payment
            {
                Id = 2,
                PaymentMethod = "Bank Transfer",
                Amount = 850.0m,
                CreatedAt = new DateTime(2024, 9, 12),
            },
        };

        [HttpGet]
        public ActionResult GetPayments()
        {
            if (payments.Count == 0)
            {
                return NotFound();
            }
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public ActionResult GetPaymentById(int id)
        {
            Payment? foundPayment = payments.FirstOrDefault(p => p.Id == id);
            if (foundPayment == null)
            {
                return NotFound();
            }
            return Ok(foundPayment);
        }

        [HttpGet("page/{pageNumber}/{pageSize}")]
        public ActionResult GetPaymentByPage(int pageNumber, int pageSize)
        {
            var pagedPayment = payments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Ok(pagedPayment);
        }

        [HttpPost]
        public ActionResult CreatePayment(Payment newPayment)
        {
            payments.Add(newPayment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = newPayment.Id }, newPayment);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePayment(int id)
        {
            Payment? foundpayment = payments.FirstOrDefault(p => p.Id == id);
            if (foundpayment == null)
            {
                return NotFound();
            }
            payments.Remove(foundpayment);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePayment(int id, Payment updatedPayment)
        {
            Payment? existingPayment = payments.FirstOrDefault(p => p.Id == id);

            if (existingPayment == null)
            {
                return NotFound();
            }

            existingPayment.PaymentMethod = updatedPayment.PaymentMethod;
            existingPayment.Amount = updatedPayment.Amount;
            existingPayment.CreatedAt = updatedPayment.CreatedAt;

            return NoContent();
        }
    }
}
