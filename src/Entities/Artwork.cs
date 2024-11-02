using System.ComponentModel.DataAnnotations;

namespace Backend_Teamwork.src.Entities
{
    public class Artwork
    {
        public Guid Id { get; set; }

        [
            Required(ErrorMessage = "Title shouldn't be null"),
            MinLength(6, ErrorMessage = "Title should be at at least 6 characters"),
            MaxLength(30, ErrorMessage = "Title shouldn't be more than 30 characters")
        ]
        public required string Title { get; set; }

        [
            Required(ErrorMessage = "Description shouldn't be null"),
            MinLength(10, ErrorMessage = "Description should be at at least 6 characters")
        ]
        public required string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity should be greater than zero.")]
        public int Quantity { get; set; }

        [Range(1.0, double.MaxValue, ErrorMessage = "Price should be greater than zero.")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "User Id shouldn't be null")]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required(ErrorMessage = "Category Id shouldn't be null")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // OrderDetails
        public List<OrderDetails>? OrderDetails { get; set; }
        public String? ImageUrl {get; set;}
    }
}
