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

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

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
            return CreatedAtAction(nameof(GetUserById), new { id = UserCreated.Id }, UserCreated);
        }

        // POST: api/v1/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserCreateDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            return Ok(token);
        }
    }
}
