namespace Backend_Teamwork.src.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; } 
        public Guid? BookingId { get; set; } 
        public Booking? Booking { get; set; }
    }
}
