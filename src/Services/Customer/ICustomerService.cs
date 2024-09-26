using static Backend_Teamwork.src.DTO.CustomerDTO;

namespace Backend_Teamwork.src.Services.Customer
{
    public interface ICustomerService
    {
        // Get all
        Task<List<CustomerReadDto>> CreateAllAsync();

        // create
        Task<CustomerReadDto> CreateOneAsync(CustomerCreateDto createDto);

        // Get by id
        Task<CustomerReadDto> GetByIdAsync(Guid id);

        // delete
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, CustomerUpdateDto updateDto);
    }
}
