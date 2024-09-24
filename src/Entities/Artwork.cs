using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Controllers
{
    public class Artwork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}