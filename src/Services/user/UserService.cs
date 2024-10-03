using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.UserDTO;
using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.Services.user
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(
            UserRepository UserRepository,
            IMapper mapper,
            IConfiguration configuration
        )
        {
            _configuration = configuration;
            _userRepository = UserRepository;
            _mapper = mapper;
        }

        // Retrieves all users
        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var UserList = await _userRepository.GetAllAsync();
            return _mapper.Map<List<User>, List<UserReadDto>>(UserList);
        }

        // Gets the total count of users
        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _userRepository.GetCountAsync();
        }

        // Retrieves users with pagination options
        public async Task<List<UserReadDto>> GetUsersByPage(PaginationOptions paginationOptions)
        {
            // Validate pagination options
            if (paginationOptions.Limit <= 0)
            {
                throw CustomException.BadRequest("Limit should be greater than 0.");
            }

            if (paginationOptions.Offset < 0)
            {
                throw CustomException.BadRequest("Offset should be 0 or greater.");
            }
            var UserList = await _userRepository.GetAllAsync(paginationOptions);
            return _mapper.Map<List<User>, List<UserReadDto>>(UserList);
        }

        // Creates a new user
        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
            if (createDto == null)
            {
                throw CustomException.BadRequest("User data cannot be null.");
            }
            // Hash password before saving to the database
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );
            var user = _mapper.Map<UserCreateDto, User>(createDto);
            user.Password = hashedPassword;
            user.Salt = salt;

            if (user.Email.EndsWith("@artify.io", StringComparison.OrdinalIgnoreCase))
            {
                user.Role = UserRole.Admin;
            }

            var UserCreated = await _userRepository.CreateOneAsync(user);
            return _mapper.Map<User, UserReadDto>(UserCreated);
        }

        // Retrieves a user by their ID
        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound("User not found.");
            }
            return _mapper.Map<User, UserReadDto>(foundUser);
        }

        // Deletes a user by their ID
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound("User not found.");
            }
            return await _userRepository.DeleteOneAsync(foundUser);
        }

        // Updates a user by their ID
        public async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound("User not found.");
            }

            // Map the update DTO to the existing User entity
            _mapper.Map(updateDto, foundUser);

            return await _userRepository.UpdateOneAsync(foundUser);
        }

        // Retrieves a user by their email
        public async Task<UserReadDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw CustomException.NotFound("User not found.");
            }
            return _mapper.Map<User, UserReadDto>(user);
        }

        // Retrieves a user by their phone number
        public async Task<UserReadDto> GetByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
            if (user == null)
            {
                throw CustomException.NotFound("User not found.");
            }
            return _mapper.Map<User, UserReadDto>(user);
        }

        // Retrieves a user by their name
        public async Task<UserReadDto> GetByNameAsync(string name)
        {
            var user = await _userRepository.GetByNameAsync(name);
            if (user == null)
            {
                throw CustomException.NotFound("User not found.");
            }
            return _mapper.Map<User, UserReadDto>(user);
        }

        // Signs in a user with their credentials
        public async Task<string> SignInAsync(UserCreateDto createDto)
        {
            var foundUser = await _userRepository.GetByEmailAsync(createDto.Email);
            if (foundUser == null)
            {
                throw CustomException.NotFound("User not found.");
            }

            // Verify the password
            bool isMatched = PasswordUtils.VerifyPassword(
                createDto.Password,
                foundUser.Password,
                foundUser.Salt
            );

            if (!isMatched)
            {
                throw CustomException.UnAuthorized("Unauthorized access.");
            }

            var TokenUtil = new TokenUtils(_configuration);
            return TokenUtil.GenerateToken(foundUser);
        }
    }
}
