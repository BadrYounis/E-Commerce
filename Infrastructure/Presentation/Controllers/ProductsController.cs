using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos;
using Shared.Enums;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        //GetAllProducts
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery] ProductSpecificationParameters parameters)
            => Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));

        //GetProductById   
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));

        //GetAllBrands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        //GetAllTypes
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());
    }
}