using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts;
public interface IOrderService
{
    Task<OrderResult> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail);
    Task<OrderResult> CreateOrderAsync(OrderRequest order, string userEmail);
    Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
}