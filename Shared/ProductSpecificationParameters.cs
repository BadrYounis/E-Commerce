using Shared.Enums;

namespace Shared;
public class ProductSpecificationParameters
{
    public int? TypeId { get; set; }
    public int? BrandId { get; set; }
    public ProductSortingOptions Sort { get; set; }
    public string? Search { get; set; }
}