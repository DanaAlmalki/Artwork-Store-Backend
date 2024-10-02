using System.Security.Claims;
using Backend_Teamwork.src.DTO;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.artwork;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.ArtworkDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtworksController : ControllerBase
    {
        /* public static List<Artwork> artworks = new List<Artwork>
        {
            new Artwork
            {
                Title = "first art work",
                Description = "first art work description",
                Quantity = 3,
                Price = 130,
                CreatedAt = DateTime.Now,
            },
            new Artwork
            {
                Title = "second art work",
                Description = "second art work description",
                Quantity = 7,
                Price = 80.5,
                CreatedAt = DateTime.Now,
            },
        }; */

        private readonly IArtworkService _artworkService;

        // Constructor
        public ArtworksController(IArtworkService service)
        {
            _artworkService = service;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<ArtworkReadDto>> CreateOne(
            [FromBody] ArtworkCreateDto createDto
        )
        {
            // extract user information
            var authenticateClaims = HttpContext.User;
            // get user id from claim
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            // string => guid
            var userGuid = new Guid(userId);

            var createdArtwork = await _artworkService.CreateOneAsync(userGuid, createDto);
            //return Created(url, createdArtwork);
            return Ok(createdArtwork);
        }

        // Get all
        [HttpGet]
        public async Task<ActionResult<List<ArtworkReadDto>>> GetAll()
        {
            var artworkList = await _artworkService.GetAllAsync();
            return Ok(artworkList);
        }

        // Get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtworkReadDto>> GetById([FromRoute] Guid id)
        {
            var artwork = await _artworkService.GetByIdAsync(id);
            return Ok(artwork);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOne(Guid id, ArtworkUpdateDTO updateDTO)
        {
            await _artworkService.UpdateOneAsync(id, updateDTO);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            await _artworkService.DeleteOneAsync(id);
            return NoContent();
        }
    }
}
