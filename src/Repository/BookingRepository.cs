using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class BookingRepository
    {
        private readonly DbSet<Booking> _booking;
        private readonly DatabaseContext _databaseContext;

        public BookingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _booking = databaseContext.Set<Booking>();
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            //return await _booking.Include(b => b.Workshop).ThenInclude(w => w.UserId).ToListAsync();
            return await _booking.Include(b => b.Workshop).Include(b=>b.User).ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _booking
                .Include(b => b.Workshop)
                .Include(b=>b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Booking>> GetByUserIdAsync(Guid userId)
        {
            return await _booking
                .Include(b => b.Workshop)
                .Include(b=>b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByStatusAsync(string status)
        {
            return await _booking
                .Include(b => b.Workshop)
                .Include(b=>b.User)
                .Where(b => b.Status.ToString().ToLower() == status.ToLower())
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByUserIdAndStatusAsync(Guid userId, string status)
        {
            return await _booking
                .Include(b => b.Workshop)
                .Include(b=>b.User)
                .Where(b => b.Status.ToString() == status.ToString() && b.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> GetByUserIdAndWorkshopIdAsync(Guid userId, Guid workshopId)
        {
            return await _booking.AnyAsync(b => b.UserId == userId && b.WorkshopId == workshopId);
        }

        public async Task<List<Booking>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _booking
                .Include(b => b.Workshop)
                .Include(b=>b.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByUserIdWithPaginationAsync(
            Guid userId,
            int pageNumber,
            int pageSize
        )
        {
            var bookings = _booking.Where(b => b.UserId == userId);
            return await bookings
                .Include(b => b.Workshop)
                .Include(b=>b.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Booking?> CreateAsync(Booking booking)
        {
            await _booking.AddAsync(booking);
            await _databaseContext.SaveChangesAsync();
            return await GetByIdAsync(booking.Id);
        }

        public async Task<Booking?> UpdateAsync(Booking booking)
        {
            _booking.Update(booking);
            await _databaseContext.SaveChangesAsync();
            return await GetByIdAsync(booking.Id);
        }
    }
}
