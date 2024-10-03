using System.Security.Claims;
using Backend_Teamwork.src.Services.user;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.UserDTO;

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
        // [Authorize(Roles = "Admin")]  // Accessible by Admin
        public async Task<ActionResult<List<UserReadDto>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin")]  // Only Admin
        public async Task<ActionResult<UserReadDto>> GetUserById([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        // GET: api/v1/users/email
        [HttpGet("email")]
        // [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> GetByEmail([FromRoute] string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            return Ok(user);
        }

        // POST: api/v1/users
        [HttpPost]
        // [AllowAnonymous] // No authorization required for signing up new users
        public async Task<ActionResult<UserReadDto>> SignUp([FromBody] UserCreateDto createDto)
        {
            var UserCreated = await _userService.CreateOneAsync(createDto);
            return CreatedAtAction(nameof(GetUserById), new { id = UserCreated.Id }, UserCreated);
        }

        // POST: api/v1/users/signin
        [HttpPost("signin")]
        // [AllowAnonymous] // No authorization required for signing in
        public async Task<ActionResult<string>> SignIn([FromBody] UserCreateDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            return Ok(token);
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateUser(
            [FromRoute] Guid id,
            [FromBody] UserUpdateDto updateDto
        )
        {
            await _userService.UpdateOneAsync(id, updateDto);
            return NoContent();
        } // should ask my teammates

        // DELETE: api/v1/users/{id}
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<bool>> DeleteUser([FromRoute] Guid id)
        {
            await _userService.DeleteOneAsync(id);
            return NoContent();
        }

        // Extra Features

        // search-by-phone-number
        [HttpGet("search-by-phone/{phoneNumber}")]
        // [Authorize(Roles = "Admin")] // Only Admin
        public async Task<ActionResult<UserReadDto>> GetByPhone([FromRoute] string phoneNumber)
        {
            var user = await _userService.GetByPhoneNumberAsync(phoneNumber);
            return Ok(user);
        }

        // GET: api/v1/users/page
        [HttpGet("pagination")]
        public async Task<ActionResult<UserReadDto>> GetUsersByPage(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var users = await _userService.GetUsersByPage(paginationOptions);
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET: api/v1/users/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalUsersCount()
        {
            var count = _userService.GetTotalUsersCountAsync();
            return Ok(count);
        }
    }
}
