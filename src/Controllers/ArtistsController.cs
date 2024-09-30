using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.artist;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.ArtistDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtistReadDto>>> GetArtists()
        {
            var artists = await _artistService.GetAllAsync();
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists);
        }

        // get  by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistReadDto>> GetArtistById(Guid id)
        {
            var foundArtist = await _artistService.GetByIdAsync(id);
            if (foundArtist == null)
            {
                return NotFound($"Artist with ID {id} not found.");
            }
            return Ok(foundArtist);
        }

        // post
        [HttpPost]
        public async Task<ActionResult<ArtistReadDto>> CreateArtist(
            [FromBody] ArtistCreateDto artistDTO
        )
        {
            var existingArtistByEmail = await _artistService.GetByEmailAsync(artistDTO.Email);
            if (existingArtistByEmail != null)
            {
                return BadRequest("Email already in use.");
            }

            var existingArtistByPhone = await _artistService.GetByPhoneNumberAsync(artistDTO.PhoneNumber);
            if (existingArtistByPhone != null)
            {
                return BadRequest("Phone number already in use.");
            }

            PasswordUtils.HashPassword(
                artistDTO.Password,
                out string hashedPassword,
                out byte[] salt
            );

            artistDTO.Password = hashedPassword;
            artistDTO.Salt = salt;

            // artists.Add(newArtist);
            var artist = await _artistService.CreateOneAsync(artistDTO);
            if (artist == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateArtist), new { id = artist.Id }, artist);
            // Artist? foundArtistByPhone = artists.FirstOrDefault(c => c.PhoneNumber == newArtist.PhoneNumber);
            // if (foundArtistByPhone != null)
            // {
            //     return BadRequest("Phone number already in use.");
            // }

        }

        // login
        [HttpPost("login")]
        public async Task<ActionResult<ArtistReadDto>> Login(ArtistCreateDto createDto)
        {
            // find with email
            var foundArtist = await _artistService.GetByEmailAsync(createDto.Email);

            if (foundArtist == null)
            {
                return NotFound();
                // 404
            }
            // تحقق من تطابق كلمة المرور
            bool isMatched = PasswordUtils.VerifyPassword(createDto.Password, foundArtist.Password, foundArtist.Salt);

            if (!isMatched)
            {
                return Unauthorized("Invalid password.");
            }

            return Ok(foundArtist);

        }

        // delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteArtist(Guid id)
        {
            var isDeleted = await _artistService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Artist with ID {id} not found.");
            }
            return NoContent();
        }

        // update
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateArtist(Guid id, ArtistUpdateDto updateDto)
        {
            var currentArtist = await _artistService.UpdateOneAsync(id, updateDto);

            if (currentArtist == null)
            {
                return NotFound($"Artist with ID {id} not found.");
            }
            return NoContent();
        }

        //custom Get Method
        // sort-by-name
        [HttpGet("sort-by-name")]
        public async Task<ActionResult<List<ArtistReadDto>>> SortArtistByName()
        {
            var artists = await _artistService.GetAllAsync();
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists.OrderBy(c => c.Name).ToList());
        }

        // sort-by-email
        [HttpGet("sort-by-email")]
        public async Task<ActionResult<List<ArtistReadDto>>> SortArtistByEmail()
        {
            var artists = await _artistService.GetAllAsync();
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists.OrderBy(c => c.Email).ToList());
        }

        // sort-by-phoneNumber
        [HttpGet("sort-by-phone-num")]
        public async Task<ActionResult<List<ArtistReadDto>>> SortArtistByPhoneNum()
        {
            var artists = await _artistService.GetAllAsync();
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists.OrderBy(c => c.PhoneNumber).ToList());
        }

        // search-by-name
        [HttpGet("search-by-name/{name}")]
        public async Task<ActionResult<ArtistReadDto>> GetArtistByName(string name)
        {
            var foundArtist = await _artistService.GetByNameAsync(name);
            if (foundArtist == null)
            {
                return NotFound();
            }
            return Ok(foundArtist);
        }

        // search-by-email
        [HttpGet("search-by-email/{email}")]
        public async Task<ActionResult<ArtistReadDto>> GetArtistByEmail(string email)
        {
            var foundArtist = await _artistService.GetByEmailAsync(email);
            if (foundArtist == null)
            {
                return NotFound();
            }
            return Ok(foundArtist);
        }

        // search-by-phone-num
        [HttpGet("search-by-phoneNum/{phoneNum}")]
        public async Task<ActionResult<ArtistReadDto>> GetArtistByPhoneNum(string phoneNum)
        {
            var foundArtist = await _artistService.GetByPhoneNumberAsync(phoneNum);
            if (foundArtist == null)
            {
                return NotFound();
            }
            return Ok(foundArtist);
        }

    }

}
