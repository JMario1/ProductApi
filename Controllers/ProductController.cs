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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            Response<Product> result = await _productService.GetAsync(id, cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            Response<List<Product>> result = await _productService.GetAllAsync(cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<CreateProductRequest, Product>(request);
            Response<Product> result = await _productService.CreateAsync(product, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), 200)]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<UpdateProductRequest, Product>(request);
            Response<Product> result = await _productService.UpdateAsync(product, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            Response<Product> result = await _productService.DeleteAsync(id, cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpGet("disabled")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> GetDisabled(CancellationToken cancellationToken)
        {
            Response<List<Product>> result = await _productService.GetDisabledAsync(cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("sum")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Sum(CancellationToken cancellationToken)
        {
            Response<int> result = await _productService.SumOfPrices(cancellationToken);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPut("disable/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Disable(int id, CancellationToken cancellationToken)
        {
            Response<Product> result = await _productService.DisableProductAsync(id, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpPut("enable/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Enable(int id, CancellationToken cancellationToken)
        {
            Response<Product> result = await _productService.EnableProductAsync(id, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}