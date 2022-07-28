using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> FindByIdAsync(int id, CancellationToken cancellationToken);
        Task<Category> FindByNameAsync(string name);
        Task<List<Category>> FindAllAsync( CancellationToken cancellationToken);
        Task AddAsync(Category category, CancellationToken cancellationToken);
    }
}