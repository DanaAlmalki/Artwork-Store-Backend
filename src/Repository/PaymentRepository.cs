using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;
namespace sda_3_online_Backend_Teamwork.src.Repository
{
    public class PaymentRepository
    {
        private readonly DbSet<Payment> _payment;
        private readonly DatabaseContext _databaseContext;
        public PaymentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _payment = databaseContext.Set<Payment>();
        }

        // create in database
        public async Task<Payment> CreateOneAsync(Payment newPayment)
        {
            await _payment.AddAsync(newPayment);
            await _databaseContext.SaveChangesAsync();
            return newPayment;
        }
        // get by id
        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _payment.FindAsync(id);
        }
        // delete 
        public async Task<bool> DeleteOneAsync(Payment deletePayment)
        {
            _payment.Remove(deletePayment);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // update 
        public async Task<bool> UpdateOneAsync(Payment updatePayment)
        {
            _payment.Update(updatePayment);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // get all 
        public async Task<List<Payment>> GetAllAsync()
        {
            return await _payment.ToListAsync();
        }
    }
}