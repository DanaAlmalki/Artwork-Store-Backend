using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class ArtworkRepository
    {
        private readonly DbSet<Artwork> _artwork;
        private readonly DatabaseContext _databaseContext; // for dependency injection

        // Dependency Injection
        public ArtworkRepository(DatabaseContext databaseContext){
            _databaseContext = databaseContext;
            // initialize artwork table in the database
            _artwork = databaseContext.Set<Artwork>();
        }

        // Methods
        // create artwork
        public async Task<Artwork> CreateOneAsync(Artwork newArtwork){
            await _artwork.AddAsync(newArtwork);
            await _databaseContext.SaveChangesAsync();
            return newArtwork;
        }

        // get artwork by id
        public async Task<List<Artwork>> GetAllAsync(){
            return await _artwork.ToListAsync();
        }

        // get artwork by id
        public async Task<Artwork?> GetByIdAsync(Guid id){
            return await _artwork.FindAsync(id);
        }

        // delete artwork
        public async Task<bool> DeleteOneAsync(Artwork artwork){
            _artwork.Remove(artwork);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // update artwork
        public async Task<bool> UpdateOneAsync(Artwork updateArtwork){
            _artwork.Update(updateArtwork);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

    }
}