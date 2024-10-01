
namespace Backend_Teamwork.src.Entities
{
    public class OrderDetails
    {
        public Guid Id { get; set; }
        public Guid ArtworkId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public Artwork Artwork { get; set; }

    }
}