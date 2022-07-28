using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> FindAsync(int id);
        Task<List<Product>> FindAllsync();
        void Remove(int id);
        Task DisableAsync(int id);
        Task EnableAsync(int id);
        void Update(Product product);
        Task AddAsync(Product product);
        Task<int> SumOfPricesAsync();
        Task<List<Product>> FindDisabledAsync();

    }
}