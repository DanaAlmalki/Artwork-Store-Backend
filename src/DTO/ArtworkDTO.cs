using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;

namespace Backend_Teamwork.src.DTO
{
    public class ArtworkDTO
    {
        // create Artwork
        public class ArtworkCreateDto
        {
            public String ImageUrl {get; set;} 
            [
                Required(ErrorMessage = "Title shouldn't be null"),
                MinLength(6, ErrorMessage = "Title should be at at least 6 characters"),
                MaxLength(30, ErrorMessage = "Title shouldn't be more than 30 characters")
            ]
            public string Title { get; set; }

            [
                Required(ErrorMessage = "Description shouldn't be null"),
                MinLength(10, ErrorMessage = "Description should be at at least 6 characters")
            ]
            public string Description { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Quantity should be greater than zero.")]
            public int Quantity { get; set; }

            [Range(1.0, double.MaxValue, ErrorMessage = "Price should be greater than zero.")]
            public decimal Price { get; set; }
            public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
            public Guid CategoryId { get; set; }
        }

        // read data (get data)
        public class ArtworkReadDto
        {
            public Guid Id { get; set; }
            public String ImageUrl {get; set;}
            public string Title { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public DateTime CreatedAt { get; set; }
            public Category Category { get; set; }
            public User User { get; set; }
        }

        // update
        public class ArtworkUpdateDTO
        {
            public String ImageUrl {get; set;} 
            [
                Required(ErrorMessage = "Title shouldn't be null"),
                MinLength(6, ErrorMessage = "Title should be at at least 6 characters"),
                MaxLength(30, ErrorMessage = "Title shouldn't be more than 30 characters")
            ]
            public string Title { get; set; }

            [
                Required(ErrorMessage = "Description shouldn't be null"),
                MinLength(10, ErrorMessage = "Description should be at at least 6 characters")
            ]
            public string Description { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Quantity should be greater than zero.")]
            public int Quantity { get; set; }

            [Range(1.0, double.MaxValue, ErrorMessage = "Price should be greater than zero.")]
            public decimal Price { get; set; }
        }

        public class ArtworkListDto {
            public List<ArtworkReadDto> Artworks {get; set;}
            public int TotalCount {get; set;}
        }
    }
}
