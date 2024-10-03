using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class OrderRepository
    {
        private readonly DbSet<Order> _order;
        private readonly DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _order = databaseContext.Set<Order>();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _order.Include(o => o.User).ToListAsync();
        }

        public async Task<Order> CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            // to see the order details
            return await _order.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> DeleteOneAsync(Order Order)
        {
            if (Order == null)
                return false;
            _order.Remove(Order);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Order updateOrder)
        {
            if (updateOrder == null)
                return false;
            _order.Update(updateOrder);
            await _databaseContext.SaveChangesAsync();
            return true;
        }



        // with Payment
        public async Task<Order> GetOrdertWithPaymentAsync(Guid orerId)
        {
            return await _databaseContext.Order
            .Include(o => o.Payment)
            .FirstOrDefaultAsync(o => o.Id == orerId);



        }
    }
}
