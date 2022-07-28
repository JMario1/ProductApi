using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Services
{
    public interface IProductService
    {
        Task<Response<Product>> CreateAsync(Product product, CancellationToken cancellationToken);
        Task<Response<Product>> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<Response<Product>> GetAsync(int id, CancellationToken cancellationToken);
        Task<Response<List<Product>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Response<Product>> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<Response<List<Product>>> GetDisabledAsync(CancellationToken cancellationToken);
        Task<Response<Product>> DisableProductAsync(int id, CancellationToken cancellationToken);
        Task<Response<Product>> EnableProductAsync(int id, CancellationToken cancellationToken);
        Task<Response<int>> SumOfPrices(CancellationToken cancellationToken);
        
    }    
}