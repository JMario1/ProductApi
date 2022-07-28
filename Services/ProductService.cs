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

        public async Task<Response<Product>> CreateAsync(Product product)
        {
            Category existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
            {
                return new Response<Product>(false, "Invalid Category", null);
            }
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", product);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while adding product: {ex.Message}", null);
            }

        }

        public async Task<Response<Product>> DeleteAsync(int id)
        {
            Product existingProduct = await _productRepository.FindAsync(id);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                _productRepository.Remove(id);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "Deleted successfully", null);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while deleting product: {ex.Message}", null);
            }
        }

        public async Task<Response<Product>> DisableProductAsync(int id)
        {
            Product existingProduct = await _productRepository.FindAsync(id);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                await _productRepository.DisableAsync(id);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", null);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while disabling product: {ex.Message}", null);
            }
        }

        public async Task<Response<Product>> EnableProductAsync(int id)
        {
            Product existingProduct = await _productRepository.FindAsync(id);
            if (existingProduct == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }
            try
            {
                await _productRepository.EnableAsync(id);
                await _unitOfWork.CompleteAsync();
                return new Response<Product>(true, "success", null);
            }
            catch (Exception ex)
            {

                return new Response<Product>(false, $"An error occurred while Enabling product: {ex.Message}", null);
            }
        }

        public async Task<Response<List<Product>>> GetAllAsync()
        {
            List<Product> products = await _productRepository.FindAllsync();

            return new Response<List<Product>>(true, "success", products);

        }

        public async Task<Response<Product>> GetAsync(int id)
        {
            Product product = await _productRepository.FindAsync(id);
            if (product == null)
            {
                return new Response<Product>(false, "Product does not exists", null);
            }

            return new Response<Product>(true, "success", product);
        }

        public async Task<Response<List<Product>>> GetDisabledAsync()
        {
            List<Product> disabledProducts = await _productRepository.FindDisabledAsync();

            return new Response<List<Product>>(true, "success", disabledProducts);
        }

        public async Task<Response<int>> SumOfPrices()
        {
            int sum  = await _productRepository.SumOfPricesAsync();

            return new Response<int>(true, "success", sum);
        }

        public async Task<Response<Product>> UpdateAsync(Product product)
        {
            Product existingProduct = await _productRepository.FindAsync(product.Id);
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

                _productRepository.Update(existingProduct);
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