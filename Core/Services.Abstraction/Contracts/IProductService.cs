using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;

namespace Services.Abstraction.Contracts;
public interface IProductService
{
    //GetAllProducts
    Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
    //GetProductById
    Task<ProductResultDto> GetProductByIdAsync(int id);
    //GetAllBrands
    Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    //GetAllTypes
    Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
}