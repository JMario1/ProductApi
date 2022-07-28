using Microsoft.EntityFrameworkCore;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Repositories;

namespace productMgtApi.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _appDbContext.Products.AddAsync(product, cancellationToken);
        }

        public async Task DisableAsync(int id, CancellationToken cancellationToken)
        {
            Product? product = await _appDbContext.Products.FindAsync(id, cancellationToken);
            if (product == null) return;
            if (product.Disabled) throw new Exception("Already disabled");
            product.Disabled = true;
            _appDbContext.Products.Update(product);
        }

        public async Task EnableAsync(int id, CancellationToken cancellationToken)
        {
            Product? product = await _appDbContext.Products.FindAsync(id, cancellationToken);
            if (product == null) return;
            if (!product.Disabled) throw new Exception("Already enabled");
            product.Disabled = false;
            _appDbContext.Products.Update(product);
        }

        public async Task<List<Product>> FindAllsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Products.Include(prod => prod.Category).Where(prod => prod.Disabled == false).ToListAsync(cancellationToken);
        }

        public async Task<Product> FindAsync(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Products.Include(prod => prod.Category).Where(prod => prod.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Product>> FindDisabledAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Products.Include(prod => prod.Category).Where(prod => prod.Disabled == true)
            .OrderByDescending(prod => prod.CreatedAt)
            .ToListAsync(cancellationToken);
        }

        public async void Remove(int id, CancellationToken cancellationToken)
        {
            Product product = await FindAsync(id, cancellationToken);
            _appDbContext.Remove(product);
        }

        public async Task<int> SumOfPricesAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Products
            .Where(prod => prod.CreatedAt.AddDays(7).Ticks >= DateTime.UtcNow.Ticks )
            .SumAsync(prod => prod.Price, cancellationToken);
        }

        public void Update(Product product, CancellationToken cancellationToken)
        {
            _appDbContext.Products.Update(product);
        }
    }
}