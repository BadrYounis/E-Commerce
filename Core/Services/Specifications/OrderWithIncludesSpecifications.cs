using Domain.Entities.OrderModule;

namespace Services.Specifications;
internal class OrderWithIncludesSpecifications : BaseSpecifications<Order, Guid>
{
    public OrderWithIncludesSpecifications(Guid id) : base(o => o.Id == id)
    {
        AddIncludes(o => o.DeliveryMethod);
        AddIncludes(o => o.OrderItems);
    }
    public OrderWithIncludesSpecifications(string userEmail) : base(o => o.UserEmail == userEmail)
    {
        AddIncludes(o => o.DeliveryMethod);
        AddIncludes(o => o.OrderItems);
        AddOrderBy(o => o.OrderDate);
    }
}