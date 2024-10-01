using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Backend_Teamwork.src.DTO.ArtworkDTO;
using static Backend_Teamwork.src.DTO.CategoryDTO;

namespace Backend_Teamwork.src.DTO
{
    public class ArtworkCategoryDTO
    {
        public class ArtworkCategoryCreateDto
        {
            public Guid ArtworkId { get; set; }
            public Guid CategoryId { get; set; }
        }

        public class ArtworkCategoryReadDto
        {
            public Guid Id { get; set; }
            public ArtworkReadDto Artwork { get; set; }
            public CategoryReadDto Category { get; set; }
        }
    }
}