using Backend_Teamwork.src.Entities;
using static Backend_Teamwork.src.DTO.ArtworkDTO;

namespace Backend_Teamwork.src.DTO
{
    public class OrderDetailDTO
    {
        public class OrderDetailCreateDto
        {
            public Guid ArtworkId { get; set; }
            public int Quantity { get; set; }
        }

        public class OrderDetailReadDto
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public ArtworkReadDto? Artwork { get; set; }
        }
        public class OrderDetailUpdateDto
        {
            public Guid ArtworkId { get; set; }
            public Guid OrderId { get; set; }
            public int Quantity { get; set; }
            public ArtworkReadDto? Artwork { get; set; }
        }
    }
}