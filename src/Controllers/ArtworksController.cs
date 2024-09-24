using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtworksController : ControllerBase
    {
        public static List<Artwork> artworks = new List<Artwork>
        {
            new Artwork
            {
                Id = 1,
                Title = "first art work",
                Description = "first art work description",
                Quantity = 3,
                Price = 130,
                CreatedAt = DateTime.Now,
            },
            new Artwork
            {
                Id = 2,
                Title = "second art work",
                Description = "second art work description",
                Quantity = 7,
                Price = 80.5,
                CreatedAt = DateTime.Now,
            },
        };

        // GET: api/v1/artworks
        [HttpGet]
        public ActionResult GetArtworks()
        {
            if (artworks.Count == 0)
            {
                return NotFound();
            }
            return Ok(artworks);
        }

        // GET: api/v1/artworks/{id}
        [HttpGet("{id}")]
        public ActionResult GetArtwork(int id)
        {
            Artwork? artwork = artworks.FirstOrDefault(a => a.Id == id);
            if (artwork == null)
            {
                return NotFound($"Artwork with ID {id} not found.");
            }
            return Ok(artwork);
        }

        // POST: api/v1/artworks
        [HttpPost]
        public ActionResult AddArtwork(Artwork artwork)
        {
            artworks.Add(artwork);
            return Created("", artwork);
        }
        
        // PUT: api/v1/artworks/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateArtwork(int id, Artwork newArtwork)
        {
            var artwork = artworks.FirstOrDefault(a => a.Id == id);
            if (artwork == null)
            {
                return NotFound($"Artwork with ID {id} not found.");
            }
            artwork.Title = newArtwork.Title;
            artwork.Description = newArtwork.Description;
            artwork.Quantity = newArtwork.Quantity;
            artwork.Price = newArtwork.Price;
            artwork.CreatedAt = newArtwork.CreatedAt;
            return NoContent();
        }

        // Delete: api/v1/artworks/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteArtwork(int id)
        {
            var artwork = artworks.FirstOrDefault(a => a.Id == id);
            if (artwork == null)
            {
                return NotFound($"Artwork with ID {id} not found.");
            }
            artworks.Remove(artwork);
            return NoContent();
        }

    }
}