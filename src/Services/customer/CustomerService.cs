using AutoMapper;
using Backend_Teamwork.src.Repository;
using static Backend_Teamwork.src.DTO.CustomerDTO;

namespace Backend_Teamwork.src.Services.customer
{
    public class CustomerService //: ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        Task<List<CustomerReadDto>> CreateAllAsync()
        {
            return null;
        }

        // create
        Task<CustomerReadDto> CreateOneAsync(CustomerCreateDto createDto)
        {
            return null;
        }

        // Get by id
        Task<CustomerReadDto> GetByIdAsync(Guid id)
        {
            return null;
        }

        // delete
        Task<bool> DeleteOneAsync(Guid id)
        {
            return null;
        }

        Task<bool> UpdateOneAsync(Guid id, CustomerUpdateDto updateDto)
        {
            return null;
        }
    }
}
