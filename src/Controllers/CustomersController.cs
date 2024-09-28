using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.customer;
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

        public static List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                //Id = 1,
                Name = "Abeer",
                PhoneNumber = "0563034777",
                Email = "abeeralialjohani@gmail.com",
                Password = "1234",
            },
            new Customer
            {
                //Id = 2,
                Name = "Shuaa",
                PhoneNumber = "0563456565",
                Email = "Shuaa@gmail.com",
                Password = "1212",
            },
            new Customer
            {
                //Id = 3,
                Name = "Manar",
                PhoneNumber = "0563434323",
                Email = "Manar@gmail.com",
                Password = "1111",
            },
            new Customer
            {
                //Id = 4,
                Name = "Danah",
                PhoneNumber = "0573434223",
                Email = "Danah@gmail.com",
                Password = "2323",
            },
            new Customer
            {
                //Id = 5,
                Name = "Bashaer",
                PhoneNumber = "0573567223",
                Email = "Bashaer@gmail.com",
                Password = "4567",
            },
        };

        // GET: api/v1/customers
        [HttpGet]
        public ActionResult GetCustomers()
        {
            if (customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // GET: api/v1/customers/{id}
        [HttpGet("{id}")]
        public ActionResult GetCustomerById(Guid id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

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
            var count = customers.Count();
            return Ok(count);
        }

        // POST: api/v1/customers
        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> SignUp(CustomerCreateDto createDto)
        {
            var customerCreated = await _customerService.CreateOneAsync(createDto);
            return Ok(customerCreated);

            // PasswordUtils.HashPassword(
            //     newCustomer.Password,
            //     out string hashedPassword,
            //     out byte[] salt
            // );
            // newCustomer.Password = hashedPassword;
            // newCustomer.Salt = salt;

            // customers.Add(newCustomer);
            // return Created($"/api/users/{newCustomer.Id}", newCustomer);
        }

        // POST: api/v1/customers/login
        [HttpPost("login")]
        public ActionResult Login(Customer customer)
        {
            Customer? foundCustomer = customers.FirstOrDefault(p => p.Email == customer.Email);
            if (foundCustomer == null)
            {
                return NotFound();
            }

            bool isMatched = PasswordUtils.VerifyPassword(
                customer.Password,
                foundCustomer.Password,
                foundCustomer.Salt
            );
            if (!isMatched)
            {
                return Unauthorized();
            }
            return Ok(foundCustomer);
        }

        // PUT: api/v1/customers/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(Guid id, Customer updateCustomer)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            customer.Name = updateCustomer.Name;
            customer.PhoneNumber = updateCustomer.PhoneNumber;
            customer.Email = updateCustomer.Email;
            customer.Password = updateCustomer.Password;

            return NoContent();
        }

        // DELETE: api/v1/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(Guid id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            customers.Remove(customer);
            return NoContent();
        }
    }
}
