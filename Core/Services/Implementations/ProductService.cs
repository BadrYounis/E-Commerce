using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos;
using Shared.Enums;

namespace Services.Implementations;
public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
{
    public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? typeId, int? brandId, ProductSortingOptions sort)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(typeId, brandId, sort);
        var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
        var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
        return productsResult;
    }
    public async Task<ProductResultDto> GetProductByIdAsync(int id)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(id);
        var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
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