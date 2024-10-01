using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Set to UTC by default
        }

        // read data (get data)
        public class ArtworkReadDto
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public DateTime CreatedAt { get; set; }
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
