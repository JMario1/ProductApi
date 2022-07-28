using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> FindByIdAsync(int id);
        Task<Category> FindByNameAsync(string name);
        Task<List<Category>> FindAllAsync();
        Task AddAsync(Category category);
    }
}