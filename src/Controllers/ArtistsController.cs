using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.artist;
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

        // public static List<Artist> artists = new List<Artist>
        // {
        //     new Artist
        //     {

        //         // Id = 11,
        //         Name = "Shuaa",
        //         Description = "",
        //         Email = "shuaa@gmail.com",
        //         PhoneNumber = "0512069567",
        //         Password = "123",
        //     },
        //     new Artist
        //     {
        //         // Id = 22,
        //         Name = "Ahmed",
        //         Description = "",
        //         Email = "Ahmed33@gmail.com",
        //         PhoneNumber = "0582749384",
        //         Password = "321",
        //     },
        //     new Artist
        //     {
        //         // Id = 33,
        //         Name = "Maha",
        //         Description = "",
        //         Email = "maha4@gmail.com",
        //         PhoneNumber = "0593749283",
        //         Password = "987",
        //     },
        // };

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
            // Artist? foundArtistByEmail = artistDTO.FirstOrDefault(c => c.Email == newArtist.Email);
            // if (foundArtistByEmail != null)
            // {
            //     return BadRequest("Email already in use.");
            // }

            // Artist? foundArtistByPhone = artists.FirstOrDefault(c => c.PhoneNumber == newArtist.PhoneNumber);
            // if (foundArtistByPhone != null)
            // {
            //     return BadRequest("Phone number already in use.");
            // }

            PasswordUtils.HashPassword(artistDTO.Password, out string hashedPassword, out byte[] salt);

            artistDTO.Password = hashedPassword;
            artistDTO.Salt = salt;



            // artists.Add(newArtist);
            var artist = await _artistService.CreateOneAsync(artistDTO);
            if (artist == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(
                nameof(CreateArtist),
                 new { id = artist.Id }, artist);
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
            // hash == plain
            // if (found.Password == user.Password)

            // check if password is match
            // plain password,
            // bool isMatched = PasswordUtils.VerifyPassword(
            //     artist.Password,
            //     foundArtist.Password,
            //     foundArtist.Salt
            // );

            // if (!isMatched)
            // {
            //     return Unauthorized();
            //     // 401
            // }

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
            // artists.Remove(foundArtist);
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

            // currentArtist.Name = updateArtist.Name;
            // currentArtist.Email = updateArtist.Email;
            // currentArtist.PhoneNumber = updateArtist.PhoneNumber;
            // currentArtist.Description = updateArtist.Description;
            // currentArtist.Password = updateArtist.Password;
            return NoContent();
        }

        //custom Get Method
        //sort-by-name
        // [HttpGet("sort-by-name")]
        // public ActionResult SortArtistByName()
        // {
        //     if (artists.Count == 0)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(artists.OrderBy(c => c.Name).ToList());
        // }
        //sort-by-email
        // [HttpGet("sort-by-email")]
        // public ActionResult SortArtistByEmail()
        // {
        //     if (artists.Count == 0)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(artists.OrderBy(c => c.Email).ToList());
        // }
        // //sort-by-phoneNumber
        // [HttpGet("sort-by-phone-num")]
        // public ActionResult SortArtistByPhoneNum()
        // {
        //     if (artists.Count == 0)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(artists.OrderBy(c => c.PhoneNumber).ToList());
        // }
        // //search-by-name
        // [HttpGet("search-by-name/{name}")]
        // public ActionResult GetArtistByName(string name)
        // {
        //     Artist? foundArtist = artists.FirstOrDefault(c => c.Name == name);
        //     if (foundArtist == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(foundArtist);
        // }
        // //search-by-email
        // [HttpGet("search-by-email/{email}")]
        // public ActionResult GetArtistByEmail(string email)
        // {
        //     Artist? foundArtist = artists.FirstOrDefault(c => c.Email == email);
        //     if (foundArtist == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(foundArtist);
        // }
        // //search-by-phone-num
        // [HttpGet("search-by-phoneNum/{phoneNum}")]
        // public ActionResult GetArtistByPhoneNum(string phoneNum)
        // {
        //     Artist? foundArtist = artists.FirstOrDefault(c => c.PhoneNumber == phoneNum);
        //     if (foundArtist == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(foundArtist);
        // }

    }

}
