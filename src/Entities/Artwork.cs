namespace Backend_Teamwork.src.Entities
{
    public class Artwork
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        // OrderDetails 
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}