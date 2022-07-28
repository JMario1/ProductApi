using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Services
{
    public interface ICategoryService
    {
        Task<Response<Category>> GetAsync(int id, CancellationToken cancellationToken);
        Task<Response<List<Category>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Response<Category>> CreateAsync(Category category, CancellationToken cancellationToken);
    }
}