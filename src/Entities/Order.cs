namespace Backend_Teamwork.src.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
