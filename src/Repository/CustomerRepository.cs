using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class CustomerRepository
    {
        private readonly DbSet<Customer> _customer;
        private readonly DatabaseContext _databaseContext;

        public CustomerRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _customer = databaseContext.Set<Customer>();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customer.ToListAsync();
        }

        public async Task<Customer> CreateOneAsync(Customer newCustomer)
        {
            await _customer.AddAsync(newCustomer);
            await _databaseContext.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _customer.FindAsync(id);
        }

        public async Task<bool> DeleteOneAsync(Customer customer)
        {
            if (customer == null)
                return false;
            _customer.Remove(customer);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Customer updateCustomer)
        {
            if (updateCustomer == null)
                return false;
            _customer.Update(updateCustomer);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _customer.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
