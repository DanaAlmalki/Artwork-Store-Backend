using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Entities;

namespace Backend_Teamwork.src.Entities
{
    public class ArtworkCategory
    {
        public Guid Id {get; set;}
        public Guid ArtworkId {get; set;}
        public Guid CategoryId {get; set;}
        public Artwork Artwork {get; set;} = null!;
        public Category Category {get; set;} = null!;
    }
}