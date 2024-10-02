using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Repository;
using static Backend_Teamwork.src.DTO.ArtworkCategoryDTO;

namespace Backend_Teamwork.src.DTO
{
    public class ArtworkDTO
    {
        // create Artwork
        public class ArtworkCreateDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public DateTime CreatedAt { get; set; }
            public List<ArtworkCategoryCreateDto>? ArtworkCategories { get; set; }
        }

        // read data (get data)
        public class ArtworkReadDto
        {
            public Guid Id { get; set; }
            public Guid ArtistId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public DateTime CreatedAt { get; set; }
            public List<ArtworkCategoryReadDto>? ArtworkCategories { get; set; }
        }

        // update
        public class ArtworkUpdateDTO
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}