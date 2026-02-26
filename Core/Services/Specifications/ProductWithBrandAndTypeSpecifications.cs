using Domain.Entities.ProductModule;

namespace Services.Specifications;
internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
{
    //Get All Products => Include Brands, Types (Include)
    public ProductWithBrandAndTypeSpecifications() : base(null)
    {
        AddIncludes(p => p.ProductBrand);
        AddIncludes(p => p.ProductType);
    }
    //Get Product By Id => Include Brand, Type (Include).Where (Criteria) 
    public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
    {
        AddIncludes(p => p.ProductBrand);
        AddIncludes(p => p.ProductType);
    }
}