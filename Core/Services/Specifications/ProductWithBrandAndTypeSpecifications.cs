using Domain.Entities.ProductModule;
using Shared.Enums;

namespace Services.Specifications;
internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
{
    //Get All Products => Include Brands, Types (Include)
    public ProductWithBrandAndTypeSpecifications(int? typeId, int? brandId, ProductSortingOptions sort)
        : base(p => (!typeId.HasValue || p.TypeId == typeId)
                 && (!brandId.HasValue || p.BrandId == brandId))
    {
        AddIncludes(p => p.ProductBrand);
        AddIncludes(p => p.ProductType);

        switch (sort)
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