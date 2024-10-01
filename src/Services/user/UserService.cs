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

        // Get all
        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var UserList = await _userRepository.GetAllAsync();
            return _mapper.Map<List<User>, List<UserReadDto>>(UserList);
        }

        // Create
        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
            var foundUserByEmail = await _userRepository.GetByEmailAsync(createDto.Email);
            var foundUserByPhoneNumber = await _userRepository.GetByPhoneNumberAsync(
                createDto.PhoneNumber
            );

            if (foundUserByEmail != null || foundUserByPhoneNumber != null)
            {
                return null;
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
            user.Role = UserRole.Customer;

            var UserCreated = await _userRepository.CreateOneAsync(
                _mapper.Map<UserCreateDto, User>(createDto)
            );
            return _mapper.Map<User, UserReadDto>(UserCreated);
        }

        // Get by id
        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserReadDto>(foundUser);
        }

        // Delete
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser == null)
                return false;
            return await _userRepository.DeleteOneAsync(foundUser);
        }

        // Update
        public async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser == null)
                return false;

            // Map the update DTO to the existing User entity
            _mapper.Map(updateDto, foundUser);

            return await _userRepository.UpdateOneAsync(foundUser);
        }

        // Get by email
        public async Task<UserReadDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<User, UserReadDto>(user);
        }

        public async Task<UserReadDto> GetByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
            return _mapper.Map<User, UserReadDto>(user);
        }

        public async Task<UserReadDto> GetByNameAsync(string name)
        {
            var user = await _userRepository.GetByNameAsync(name);
            return _mapper.Map<User, UserReadDto>(user);
        }

        public async Task<string> SignInAsync(UserCreateDto createDto)
        {
            var foundUser = await _userRepository.GetByEmailAsync(createDto.Email);
            if (foundUser == null)
            {
                return "NotFound";
            }

            // Verify the password
            bool isMatched = PasswordUtils.VerifyPassword(
                createDto.Password,
                foundUser.Password,
                foundUser.Salt
            );

            if (!isMatched)
            {
                return "Unauthorized";
            }

            var TokenUtil = new TokenUtils(_configuration);
            return TokenUtil.GenerateToken(foundUser);
        }
    }
}
