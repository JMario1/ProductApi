using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Services
{
    public interface ICategoryService
    {
        Task<Response<Category>> GetAsync(int id);
        Task<Response<List<Category>>> GetAllAsync();
        Task<Response<Category>> CreateAsync(Category category);
    }
}