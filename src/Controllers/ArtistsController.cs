using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Teamwork.src.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    // base endpoint: api/v1/

    public class ArtistsController : ControllerBase
    {

        public static List<Artist> artists = new List<Artist>
        {
            new Artist { Id = 11, Name = "Shuaa" ,Description = "",Email= "shuaa@gmail.com"
            ,PhoneNumber = "0512069567",Password = "123"},
             new Artist { Id = 22, Name = "Ahmed" ,Description = "",Email= "Ahmed33@gmail.com"
             ,PhoneNumber = "0582749384",Password = "321"},
              new Artist { Id = 33, Name = "Maha" ,Description = "",Email= "maha4@gmail.com"
              ,PhoneNumber = "0593749283",Password = "987"}

               };
        [HttpGet]
        public ActionResult GetArtists()
        {
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists);

        }

        // get  by id
        [HttpGet("{id}")]
        public ActionResult GetrtistById(int id)
        {
            Artist? foundArtist = artists.FirstOrDefault(p => p.Id == id);
            if (foundArtist == null)
            {
                return NotFound($"Artist with ID {id} not found.");

            }
            return Ok(foundArtist);
        }

        // post

        [HttpPost]
        public ActionResult SignUpArtist(Artist newArtist)
        {
            Artist? foundArtistByEmail = artists.FirstOrDefault(c => c.Email == newArtist.Email);
            if (foundArtistByEmail != null)
            {
                return BadRequest("Email already in use.");
            }

            Artist? foundArtistByPhone = artists.FirstOrDefault(c => c.PhoneNumber == newArtist.PhoneNumber);
            if (foundArtistByPhone != null)
            {
                return BadRequest("Phone number already in use.");

            }
            // plain password: 123
            // hashed => 
            // HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
            PasswordUtils.HashPassword(newArtist.Password, out string hashedPassword, out byte[] salt);

            //update
            // 123 = 12u37595uf3ht3484hf
            newArtist.Password = hashedPassword;

            // null = hmac.Key
            newArtist.Salt = salt;
            // update id to 5

            artists.Add(newArtist);

            return Created("", "Artist created successfully");
            // return CreatedAtAction(nameof(GetrtistById), new { id = newArtist.Id }, newArtist);
        }
        // login
        [HttpPost("login")]
        public ActionResult LogInArtist(Artist artist)
        {
            // find with email
            Artist? foundArtist = artists.FirstOrDefault(p => p.Email == artist.Email);

            if (foundArtist == null)
            {
                return NotFound();
                // 404

            }
            // hash == plain
            // if (found.Password == user.Password)

            // check if password is match
            // plain password, 
            bool isMatched = PasswordUtils.VerifyPassword(artist.Password, foundArtist.Password, foundArtist.Salt);

            if (!isMatched)
            {
                return Unauthorized();
                // 401
            }

            return Ok(foundArtist);
        }
        // delete
        [HttpDelete("{id}")]
        public ActionResult DeleteArtist(int id)
        {
            Artist? foundArtist = artists.FirstOrDefault(p => p.Id == id);
            if (foundArtist == null)
            {
                return NotFound($"Artist with ID {id} not found.");
            }
            artists.Remove(foundArtist);
            return NoContent();

        }
        // update
        [HttpPut("{id}")]
        public ActionResult UpdateArtist(int id, Artist updateArtist)
        {
            Artist? currentArtist = artists.FirstOrDefault(p => p.Id == id);
            if (currentArtist == null)
            {
                return NotFound($"Artist with ID {id} not found.");
            }

            currentArtist.Name = updateArtist.Name;
            currentArtist.Email = updateArtist.Email;
            currentArtist.PhoneNumber = updateArtist.PhoneNumber;
            currentArtist.Description = updateArtist.Description;
            currentArtist.Password = updateArtist.Password;
            return NoContent();

        }

        //custom Get Method
        //sort-by-name
        [HttpGet("sort-by-name")]
        public ActionResult SortArtistByName()
        {
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists.OrderBy(c => c.Name).ToList());
        }
        //sort-by-email
        [HttpGet("sort-by-email")]
        public ActionResult SortArtistByEmail()
        {
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists.OrderBy(c => c.Email).ToList());
        }
        //sort-by-phoneNumber
        [HttpGet("sort-by-phone-num")]
        public ActionResult SortArtistByPhoneNum()
        {
            if (artists.Count == 0)
            {
                return NotFound();
            }
            return Ok(artists.OrderBy(c => c.PhoneNumber).ToList());
        }
        //search-by-name
        [HttpGet("search-by-name/{name}")]
        public ActionResult GetArtistByName(string name)
        {
            Artist? foundArtist = artists.FirstOrDefault(c => c.Name == name);
            if (foundArtist == null)
            {
                return NotFound();
            }
            return Ok(foundArtist);
        }
        //search-by-email
        [HttpGet("search-by-email/{email}")]
        public ActionResult GetArtistByEmail(string email)
        {
            Artist? foundArtist = artists.FirstOrDefault(c => c.Email == email);
            if (foundArtist == null)
            {
                return NotFound();
            }
            return Ok(foundArtist);
        }
        //search-by-phone-num
        [HttpGet("search-by-phoneNum/{phoneNum}")]
        public ActionResult GetArtistByPhoneNum(string phoneNum)
        {
            Artist? foundArtist = artists.FirstOrDefault(c => c.PhoneNumber == phoneNum);
            if (foundArtist == null)
            {
                return NotFound();
            }
            return Ok(foundArtist);
        }
    }

}
