using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;
namespace Backend_Teamwork.src.Repository
{

    public class WorkshopRepository
    {
        private readonly DbSet<Workshop> _workshops;
        private readonly DatabaseContext _databaseContext;
        public WorkshopRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _workshops = databaseContext.Set<Workshop>();
        }

        // create in database
        public async Task<Workshop> CreateOneAsync(Workshop newWorkshop)
        {
            await _workshops.AddAsync(newWorkshop);
            await _databaseContext.SaveChangesAsync();
            return newWorkshop;
        }
        // get by id
        public async Task<Workshop?> GetByIdAsync(Guid id)
        {
            return await _workshops.FindAsync(id);
        }
        // delete 
        public async Task<bool> DeleteOneAsync(Workshop deleteWorkshop)
        {
            _workshops.Remove(deleteWorkshop);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // update 
        public async Task<bool> UpdateOneAsync(Workshop updateWorkshop)
        {
            _workshops.Update(updateWorkshop);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        // get all 
        public async Task<List<Workshop>> GetAllAsync()
        {
            return await _workshops.ToListAsync();
        }



    }
}