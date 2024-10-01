using Backend_Teamwork.src.Entities;

namespace Backend_Teamwork.src.DTO
{
    public class OrderDTO
    {
        // DTO for creating a new order
        public class OrderCreateDto
        {
            public decimal TotalAmount { get; set; }
            public string? ShippingAddress { get; set; }
            public DateTime CreatedAt { get; set; }
            public Guid CustomerId { get; set; }
        }

        // DTO for reading order data
        public class OrderReadDto
        {
            public Guid Id { get; set; }
            public decimal TotalAmount { get; set; }
            public string? ShippingAddress { get; set; }
            public DateTime CreatedAt { get; set; }
            public Customer CustomerId { get; set; }
        }

        // DTO for updating an existing order
        public class OrderUpdateDto
        {
            public decimal TotalAmount { get; set; }
            public string? ShippingAddress { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
