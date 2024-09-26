using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class OrderRespository
    {
        private readonly DbSet<Order> _order;
        private readonly DatabaseContext _databaseContext;

        public OrderRespository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _order = databaseContext.Set<Order>();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _order.ToListAsync();
        }

        public async Task<Order> CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _order.FindAsync(id);
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
    }
}
