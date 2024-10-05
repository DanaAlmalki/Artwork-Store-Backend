using Backend_Teamwork.src.Entities;

namespace Backend_Teamwork.src.DTO
{
    public class PaymentDTO
    {
        public class PaymentCreateDTO
        {
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public DateTime? CreatedAt { get; set; } = DateTime.Now;
            public Guid? OrderId { get; set; } = Guid.Empty;
            public Guid? BookingId { get; set; } = Guid.Empty;
        }

        public class PaymentReadDTO
        {
            public Guid Id { get; set; }
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedAt { get; set; }
            public Order? Order { get; set; }
            public Booking? Booking { get; set; }
        }

        public class PaymentUpdateDTO
        {
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
