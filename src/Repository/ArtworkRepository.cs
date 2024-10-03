using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Utils;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class ArtworkRepository
    {
        private readonly DbSet<Artwork> _artwork;
        private readonly DatabaseContext _databaseContext; // for dependency injection

        // Dependency Injection
        public ArtworkRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            // initialize artwork table in the database
            _artwork = databaseContext.Set<Artwork>();
        }

        // Methods
        // create artwork
        public async Task<Artwork> CreateOneAsync(Artwork newArtwork)
        {
            await _artwork.AddAsync(newArtwork);
            await _databaseContext.SaveChangesAsync();
            // return newArtwork;
            return await _artwork
               .Include(o => o.Category)
               .FirstOrDefaultAsync(o => o.Id == newArtwork.Id);
        }

        // get all artworks
        public async Task<List<Artwork>> GetAllAsync(PaginationOptions paginationOptions)
        {
            // search by title
            var artworkSearch = _artwork.Where(a => a.Title.Contains(paginationOptions.Search));

            // price range
            artworkSearch = artworkSearch.Where(a => a.Price >= paginationOptions.LowPrice && a.Price <= paginationOptions.HighPrice);

            // date range
            // artworkSearch = artworkSearch.Where(a => a.CreatedAt >= paginationOptions.StartDate && a.CreatedAt <= paginationOptions.EndDate);

            // pagination
            artworkSearch = artworkSearch.Skip((paginationOptions.PageNumber - 1) * paginationOptions.PageSize).Take(paginationOptions.PageSize);

            // sort
            artworkSearch = paginationOptions.SortOrder switch
            {
                "name_desc" => artworkSearch.OrderByDescending(a => a.Title),
                "date" => artworkSearch.OrderBy(a => a.CreatedAt),
                "date_desc" => artworkSearch.OrderByDescending(a => a.CreatedAt),
                "price" => artworkSearch.OrderBy(a => a.Price),
                "price_desc" => artworkSearch.OrderByDescending(a => a.Price),
                // name ascending
                _ => artworkSearch.OrderBy(a => a.Title),
            };

            return await artworkSearch.ToListAsync();
        }

        // get artwork by id
        public async Task<Artwork?> GetByIdAsync(Guid id)
        {
            return await _artwork.FindAsync(id);
        }

        public async Task<List<Artwork>> GetByArtistIdAsync(Guid id)
        {
            return await _artwork.Include(a => a.Category).Where(a => a.ArtistId == id).ToListAsync();
        }

        // delete artwork
        public async Task<bool> DeleteOneAsync(Artwork artwork)
        {
            _artwork.Remove(artwork);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // update artwork
        public async Task<Artwork?> UpdateOneAsync(Artwork updateArtwork)
        {
            _artwork.Update(updateArtwork);
            await _databaseContext.SaveChangesAsync();
            return await _artwork.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == updateArtwork.Id);
        }
    }
}
