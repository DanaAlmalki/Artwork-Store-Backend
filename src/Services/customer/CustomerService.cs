using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.CustomerDTO;

namespace Backend_Teamwork.src.Services.customer
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(CustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // Get all
        public async Task<List<CustomerReadDto>> GetAllAsync()
        {
            var customerList = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<Customer>, List<CustomerReadDto>>(customerList);
        }

        // Create
        public async Task<CustomerReadDto> CreateOneAsync(CustomerCreateDto createDto)
        {
            // Hash password before saving to the database
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );
            var customer = _mapper.Map<CustomerCreateDto, Customer>(createDto);
            customer.Password = hashedPassword;
            customer.Salt = salt;

            var customerCreated = await _customerRepository.CreateOneAsync(
                _mapper.Map<CustomerCreateDto, Customer>(createDto)
            );
            return _mapper.Map<Customer, CustomerReadDto>(customerCreated);
        }

        // Get by id
        public async Task<CustomerReadDto> GetByIdAsync(Guid id)
        {
            var foundCustomer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<Customer, CustomerReadDto>(foundCustomer);
        }

        // Delete
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCustomer = await _customerRepository.GetByIdAsync(id);
            if (foundCustomer == null)
                return false;
            return await _customerRepository.DeleteOneAsync(foundCustomer);
        }

        // Update
        public async Task<bool> UpdateOneAsync(Guid id, CustomerUpdateDto updateDto)
        {
            var foundCustomer = await _customerRepository.GetByIdAsync(id);
            if (foundCustomer == null)
                return false;

            // Map the update DTO to the existing customer entity
            _mapper.Map(updateDto, foundCustomer);

            return await _customerRepository.UpdateOneAsync(foundCustomer);
            ;
        }

        // Get by email
        public async Task<CustomerReadDto> GetByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return _mapper.Map<Customer, CustomerReadDto>(customer);
        }
    }
}
