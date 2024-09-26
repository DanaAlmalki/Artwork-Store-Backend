namespace Backend_Teamwork.src.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
