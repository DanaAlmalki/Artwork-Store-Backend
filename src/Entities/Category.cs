using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Entities
{
    public class Category
    {
        public Guid Id { set; get; }
        [
            Required(ErrorMessage = "Name shouldn't be null"),
            MinLength(2, ErrorMessage = "Name should be at at least 2 characters"),
            MaxLength(10, ErrorMessage = "Name shouldn't be more than 10 characters")
        ]
        public string Name { set; get; }
    }
}
