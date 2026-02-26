using Shared.Dtos;
using Shared.Enums;

namespace Services.Abstraction.Contracts;
public interface IProductService
{
    //GetAllProducts
    Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? typeId, int? brandId, ProductSortingOptions sort);
    //GetProductById
    Task<ProductResultDto> GetProductByIdAsync(int id);
    //GetAllBrands
    Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    //GetAllTypes
    Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
}