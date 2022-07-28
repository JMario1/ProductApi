using Microsoft.EntityFrameworkCore;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Repositories;

namespace productMgtApi.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public  CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Category category)
        {
            await _appDbContext.Categories.AddAsync(category);
        }

        public  async Task<List<Category>> FindAllAsync()
        {
            List<Category> categories =  await _appDbContext.Categories.ToListAsync();
            return  categories;
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _appDbContext.Categories.FindAsync(id);
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await _appDbContext.Categories.Where(cat => cat.Name == name).FirstAsync();
        }
    }
}