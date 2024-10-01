using Backend_Teamwork.src.Services.user;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.UserDTO;
using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // DI
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        // GET: api/v1/users
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<UserReadDto>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserReadDto>> GetUserById([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // GET: api/v1/users/email
        [HttpGet("email")]
        [Authorize]
        public async Task<ActionResult<UserReadDto>> GetEmail([FromRoute] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required");
            }
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }
            return Ok(user);
        }

        // POST: api/v1/users
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> SignUp([FromBody] UserCreateDto createDto)
        {
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );

            createDto.Password = hashedPassword;
            createDto.Salt = salt;

            var UserCreated = await _userService.CreateOneAsync(createDto);
            if (UserCreated == null)
            {
                return BadRequest("Failed to create user. Phone number and Email should be unique");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = UserCreated.Id }, UserCreated);
        }

        // POST: api/v1/users/signin
        [HttpPost("signin")]
        public async Task<ActionResult<string>> Login([FromBody] UserCreateDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            return Ok(token);
        }

        // POST: api/v1/users/creat-admin
        [HttpPost("create-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto createDto)
        {
            // Hash the password before saving
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );

            createDto.Password = hashedPassword;
            createDto.Salt = salt;
            createDto.Role = UserRole.Admin; // Set role as 'Admin'

            var adminCreated = await _userService.CreateOneAsync(createDto);
            if (adminCreated == null)
            {
                return BadRequest("Failed to create admin");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = adminCreated.Id }, adminCreated);
        }

        // PUT: api/v1/users/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateUser(
            [FromRoute] Guid id,
            [FromBody] UserUpdateDto updateDto
        )
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid user ID");
            }

            var updateUser = await _userService.UpdateOneAsync(id, updateDto);
            if (!updateUser)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return NoContent();
        }

        // DELETE: api/v1/users/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteUser([FromRoute] Guid id)
        {
            var isDeleted = await _userService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return NoContent();
        }

        // Extra Feat
    }
}
