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

        public async Task AddAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
        }

        public async Task DisableAsync(int id)
        {
            Product? product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return;
            if (product.Disabled) throw new Exception("Already disabled");
            product.Disabled = true;
            _appDbContext.Products.Update(product);
        }

        public async Task EnableAsync(int id)
        {
            Product? product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return;
            if (!product.Disabled) throw new Exception("Already enabled");
            product.Disabled = false;
            _appDbContext.Products.Update(product);
        }

        public async Task<List<Product>> FindAllsync()
        {
            return await _appDbContext.Products.Include(prod => prod.Category).Where(prod => prod.Disabled == false).ToListAsync();
        }

        public async Task<Product> FindAsync(int id)
        {
            return await _appDbContext.Products.Include(prod => prod.Category).Where(prod => prod.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> FindDisabledAsync()
        {
            return await _appDbContext.Products.Include(prod => prod.Category).Where(prod => prod.Disabled == true)
            .OrderByDescending(prod => prod.CreatedAt)
            .ToListAsync();
        }

        public async void Remove(int id)
        {
            Product product = await FindAsync(id);
            _appDbContext.Remove(product);
        }

        public async Task<int> SumOfPricesAsync()
        {
            return await _appDbContext.Products
            .Where(prod => prod.CreatedAt.AddDays(7).Ticks >= DateTime.UtcNow.Ticks )
            .SumAsync(prod => prod.Price);
        }

        public void Update(Product product)
        {
            _appDbContext.Products.Update(product);
        }
    }
}