using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class ArtistRepository
    {
        // table - Artist
        protected DbSet<Artist> _artist;
        protected DatabaseContext _databaseContext;


        // DI
        public ArtistRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            // initialize the Artist in database
            _artist = databaseContext.Set<Artist>();
        }



        // create category
        public async Task<Artist> CreateOneAsync(Artist newArtist)
        {

            // add new artist in Artist table 
            await _artist.AddAsync(newArtist);
            // save change
            await _databaseContext.SaveChangesAsync();
            return newArtist;

        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _artist.ToListAsync();
        }

        // get id
        public async Task<Artist?> GetByIdAsync(Guid id)
        {
            return await _artist.FindAsync(id);
        }


        // delete 
        public async Task<bool> DeleteOneAsync(Artist artist)
        {
            _artist.Remove(artist);
            await _databaseContext.SaveChangesAsync();
            return true;
        }


        // update
        public async Task<bool> UpdateOneAsync(Artist updateArtist)
        {
            _artist.Update(updateArtist);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

          public async Task<Artist?> GetByNameAsync(string name)
        {
            return await _artist.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<Artist?> GetByEmailAsync(string email)
        {
            return await _artist.FirstOrDefaultAsync(c => c.Email == email);
        }
          public async Task<Artist?> GetByPhoneNumberAsync(string phoneNum)
        {
            return await _artist.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNum);
        }
        
        
    }
}
