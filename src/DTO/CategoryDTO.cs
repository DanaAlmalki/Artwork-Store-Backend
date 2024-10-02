using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.DTO
{
    public class CategoryDTO
    {
        public class CategoryCreateDto
        {
            public string Name { get; set; }
        }

        public class CategoryUpdateDto
        {
            public string Name { get; set; }
        }

        public class CategoryReadDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
