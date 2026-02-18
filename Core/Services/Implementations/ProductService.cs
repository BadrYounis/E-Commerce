using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Shared.Dtos;

namespace Services.Implementations;
public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
{
    public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
        var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
        return productsResult;
    }
    public async Task<ProductResultDto> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
        var productResult = _mapper.Map<ProductResultDto>(product);
        return productResult;
    }
    public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
    {
        //1) UnitOfWork => GenericRepository() => GetAllBrands() =>IEnumerable<ProductBrand>
        var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
        //2) Mapping(IEnumerable<ProductBrand> => IEnumerable<BrandResultDto>) => AutoMapper
        var brandsResult = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
        return brandsResult;
    }
    public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
    {
        var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
        var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
        return typesResult;
    }
}