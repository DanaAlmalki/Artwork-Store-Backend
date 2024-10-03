
namespace Backend_Teamwork.src.Entities
{
    public class OrderDetails
    {
        public Guid Id { get; set; }
        public Artwork? Artwork { get; set; }
        public Guid ArtworkId { get; set; }
        public Order? Order { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }

    }
}