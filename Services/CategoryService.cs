using productMgtApi.Domain.Models;
using productMgtApi.Domain.Repositories;
using productMgtApi.Domain.Services;

namespace productMgtApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Category>> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            Category existingCategory = await _categoryRepository.FindByNameAsync(category.Name);
            if (existingCategory == null)
            {
                try
                {
                    await _categoryRepository.AddAsync(category, cancellationToken);
                    await _unitOfWork.CompleteAsync();
                    return new Response<Category>(true, "success", category);
                }
                catch (Exception ex)
                {
                    return new Response<Category>(false, $"An error occurred while adding category: {ex.Message}", null);
                    
                }
            }

            return new Response<Category>(false, "Category name already exists", null);
        }

        public async Task<Response<List<Category>>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Category> categories = await _categoryRepository.FindAllAsync(cancellationToken);
            return new Response<List<Category>>(true, "success", categories);
        }

        public async Task<Response<Category>> GetAsync(int id, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.FindByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return new Response<Category>(false, "Not found", null);
            }
            return new Response<Category>(true, "success", category);
        }
    }
}