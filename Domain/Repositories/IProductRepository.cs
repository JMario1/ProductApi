using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> FindAsync(int id, CancellationToken cancellationToken);
        Task<List<Product>> FindAllsync(CancellationToken cancellationToken);
        void Remove(int id, CancellationToken cancellationToken);
        Task DisableAsync(int id, CancellationToken cancellationToken);
        Task EnableAsync(int id, CancellationToken cancellationToken);
        void Update(Product product, CancellationToken cancellationToken);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task<int> SumOfPricesAsync(CancellationToken cancellationToken);
        Task<List<Product>> FindDisabledAsync( CancellationToken cancellationToken);

    }
}