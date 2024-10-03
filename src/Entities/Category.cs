using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Teamwork.src.Entities
{
    public class Category
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public List<ArtworkCategory> ArtworkCategories { get; set; } = new List<ArtworkCategory>();

    }
}
