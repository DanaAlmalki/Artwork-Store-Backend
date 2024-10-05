namespace Backend_Teamwork.src.Entities
{
    public class Artwork
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // OrderDetails
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
