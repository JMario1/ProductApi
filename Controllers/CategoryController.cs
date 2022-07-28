using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using productMgtApi.Controllers.Resources;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Services;

namespace productMgtApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Response<Category> result = await _categoryService.GetAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IActionResult> GetAllAsync()
        {
            Response<List<Category>> result = await _categoryService.GetAllAsync();
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Category), 200)]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request)
        {
            Category category = _mapper.Map<CreateCategoryRequest, Category>(request);
            Response<Category> result = await _categoryService.CreateAsync(category);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}