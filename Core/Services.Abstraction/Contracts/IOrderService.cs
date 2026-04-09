using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts;
public interface IOrderService
{
    Task<OrderResult> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail);
    Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail);
}