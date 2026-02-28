using Shared.Enums;

namespace Shared;
public class ProductSpecificationParameters
{
    private const int defaultPageSize = 5;
    private const int maxPageSize = 10;
    public int? TypeId { get; set; }
    public int? BrandId { get; set; }
    public ProductSortingOptions Sort { get; set; }
    public string? Search { get; set; }
    public int PageIndex { get; set; } = 1;

    private int _pageSize = defaultPageSize;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > maxPageSize ? maxPageSize : value; }
    }
}