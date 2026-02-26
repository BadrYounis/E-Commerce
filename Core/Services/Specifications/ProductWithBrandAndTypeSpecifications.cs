using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;

namespace Services.Specifications;
internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
{
    //Get All Products => Include Brands, Types (Include)
    public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters)
        : base(p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                 && (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId))
    {
        AddIncludes(p => p.ProductBrand);
        AddIncludes(p => p.ProductType);

        switch (parameters.Sort)
        {
            case ProductSortingOptions.NameAsc:
                AddOrderBy(p => p.Name);
                break;

            case ProductSortingOptions.NameDesc:
                AddOrderByDescending(p => p.Name);
                break;

            case ProductSortingOptions.PriceAsc:
                AddOrderBy(p => p.Price);
                break;

            case ProductSortingOptions.PriceDesc:
                AddOrderByDescending(p => p.Price);
                break;

            default:
                break;
        }
    }
    //Get Product By Id => Include Brand, Type (Include).Where (Criteria) 
    public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
    {
        AddIncludes(p => p.ProductBrand);
        AddIncludes(p => p.ProductType);
    }
}