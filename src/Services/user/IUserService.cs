using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.UserDTO;

namespace Backend_Teamwork.src.Services.user
{
    public interface IUserService
    {
        // Get all
        Task<List<UserReadDto>> GetAllAsync();

        Task<List<UserReadDto>> GetUsersByPage(PaginationOptions paginationOptions);

        // create
        Task<UserReadDto> CreateOneAsync(UserCreateDto createDto);

        // Get by id
        Task<UserReadDto> GetByIdAsync(Guid id);

        // delete
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto);

        Task<UserReadDto> GetByEmailAsync(string email);
        Task<UserReadDto> GetByPhoneNumberAsync(string phoneNumber);
        Task<int> GetTotalUsersCountAsync();

        Task<string> SignInAsync(UserCreateDto createDto);
    }
}
