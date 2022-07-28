using productMgtApi.Domain.Models;
using productMgtApi.Domain.Repositories;
using productMgtApi.Domain.Services;

namespace productMgtApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<Product>> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            Category existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                return new Response<Product>(false, "Invalid Category", null);
            }
            try
            {
                await _productRepository.AddAsync(product, cancellationToken);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", product);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while adding product: {ex.Message}", null);
            }

        }

        public async Task<Response<Product>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Product existingProduct = await _productRepository.FindAsync(id, cancellationToken);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                _productRepository.Remove(id, cancellationToken);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "Deleted successfully", null);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while deleting product: {ex.Message}", null);
            }
        }

        public async Task<Response<Product>> DisableProductAsync(int id, CancellationToken cancellationToken)
        {
            Product existingProduct = await _productRepository.FindAsync(id, cancellationToken);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                await _productRepository.DisableAsync(id, cancellationToken);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", null);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while disabling product: {ex.Message}", null);
            }
        }

        public async Task<Response<Product>> EnableProductAsync(int id, CancellationToken cancellationToken)
        {
            Product existingProduct = await _productRepository.FindAsync(id, cancellationToken);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                await _productRepository.EnableAsync(id, cancellationToken);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", null);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while Enabling product: {ex.Message}", null);
            }
        }

        public async Task<Response<List<Product>>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository.FindAllsync(cancellationToken);

            return new Response<List<Product>>(true, "success", products);

        }

        public async Task<Response<Product>> GetAsync(int id, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.FindAsync(id, cancellationToken);
            if (product == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }

            return new Response<Product>(true, "success", product);
        }

        public async Task<Response<List<Product>>> GetDisabledAsync(CancellationToken cancellationToken)
        {
            List<Product> disabledProducts = await _productRepository.FindDisabledAsync(cancellationToken);

            return new Response<List<Product>>(true, "success", disabledProducts);
        }

        public async Task<Response<int>> SumOfPrices(CancellationToken cancellationToken)
        {
            int sum  = await _productRepository.SumOfPricesAsync(cancellationToken);

            return new Response<int>(true, "success", sum);
        }

        public async Task<Response<Product>> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            Product existingProduct = await _productRepository.FindAsync(product.Id, cancellationToken);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                existingProduct.Name = product.Name;
                existingProduct.AvaliableStock = product.AvaliableStock;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Price = product.Price;

                _productRepository.Update(existingProduct, cancellationToken);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", existingProduct);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while Enabling product: {ex.Message}", null);
            }
        }
    }
}