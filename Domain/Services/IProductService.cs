using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Services
{
    public interface IProductService
    {
        Task<Response<Product>> CreateAsync(Product product);
        Task<Response<Product>> UpdateAsync(Product product);
        Task<Response<Product>> GetAsync(int id);
        Task<Response<List<Product>>> GetAllAsync();
        Task<Response<Product>> DeleteAsync(int id);
        Task<Response<List<Product>>> GetDisabledAsync();
        Task<Response<Product>> DisableProductAsync(int id);
        Task<Response<Product>> EnableProductAsync(int id);
        Task<Response<int>> SumOfPrices();
        
    }    
}