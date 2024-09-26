using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class CategoryRepository
    {
        private readonly DbSet<Category> _category;
        private readonly DatabaseContext _databaseContext;

        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _category = _databaseContext.Set<Category>();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _category.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _category.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<List<Category>> GetByPageAsync(int pageNumber, int pageSize)
        {
            return await _category
                .Skip(pageNumber - 1)
                .Take(pageSize)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<List<Category>> SortAllAsync()
        {
            return await _category.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _category.AddAsync(category);
            await _databaseContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            _category.Update(category);
            await _databaseContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundcCategory = await _category.FirstOrDefaultAsync(c => c.Id == id);
            _category.Remove(foundcCategory);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
