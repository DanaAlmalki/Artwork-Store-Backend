using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.customer;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.CustomerDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        // DI
        public CustomersController(ICustomerService service)
        {
            _customerService = service;
        }

        // GET: api/v1/customers
        [HttpGet]
        public async Task<ActionResult<List<CustomerReadDto>>> GetCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            if (customers == null || !customers.Any())
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // GET: api/v1/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerReadDto>> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

        // Extra Features
        /*
                // GET: api/v1/customers/search/{name}
                [HttpGet("search/{name}")]
                public ActionResult SearchCustomers(string name)
                {
                    var matchedCustomers = customers
                        .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                        .ToList();
        
                    if (matchedCustomers == null)
                    {
                        return NotFound("No customer found with the specified name.");
                    }
                    return Ok(matchedCustomers);
                }
        
                // GET: api/v1/customers/page/{pageNumber}/{pageSize}
                [HttpGet("page/{pageNumber}/{pageSize}")]
                public ActionResult GetCustomersByPage(int pageNumber, int pageSize)
                {
                    var pagedCustomers = customers
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                    return Ok(pagedCustomers);
                }
        
                // GET: api/v1/customers/count
                [HttpGet("count")]
                public ActionResult GetTotalCustomersCount()
                {
                    var count = customers.Count;
                    return Ok(count);
                }
        */

        // POST: api/v1/customers
        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> SignUp(CustomerCreateDto createDto)
        {
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );

            createDto.Password = hashedPassword;
            createDto.Salt = salt;

            var customerCreated = await _customerService.CreateOneAsync(createDto);
            return CreatedAtAction(
                nameof(GetCustomerById),
                new { id = customerCreated.Id },
                customerCreated
            );
        }

        // POST: api/v1/customers/login
        [HttpPost("login")]
        public async Task<ActionResult<CustomerReadDto>> Login(CustomerCreateDto createDto)
        {
            var foundCustomer = await _customerService.GetByEmailAsync(createDto.Email);
            if (foundCustomer == null)
            {
                return NotFound("Customer with the provided email not found.");
            }

            // // Verify the password
            // bool isMatched = PasswordUtils.VerifyPassword(
            //     createDto.Password,
            //     foundCustomer.Password,
            //     foundCustomer.Salt
            // );

            // if (!isMatched)
            // {
            //     return Unauthorized("Incorrect password.");
            // }

            return Ok(foundCustomer);
        }

        // PUT: api/v1/customers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateCustomer(Guid id, CustomerUpdateDto updateDto)
        {
            var updateCustomer = await _customerService.UpdateOneAsync(id, updateDto);

            if (!updateCustomer)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return NoContent();
        }

        // DELETE: api/v1/customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCustomer(Guid id)
        {
            var isDeleted = await _customerService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return NoContent();
        }

        // GET: api/customer/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<CustomerReadDto>> GetCustomerByEmail(string email)
        {
            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null)
            {
                return NotFound($"Customer with email {email} not found.");
            }

            return Ok(customer);
        }
    }
}
