using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class UserRepository
    {
        private readonly DbSet<User> _user;
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _user = databaseContext.Set<User>();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _user.ToListAsync();
        }

        public async Task<User> CreateOneAsync(User newUser)
        {
            await _user.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _user.FindAsync(id);
        }

        public async Task<bool> DeleteOneAsync(User User)
        {
            if (User == null)
                return false;
            _user.Remove(User);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(User updateUser)
        {
            if (updateUser == null)
                return false;
            _user.Update(updateUser);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _user.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _user.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }
    }
}
