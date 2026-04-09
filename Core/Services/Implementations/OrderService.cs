using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;

namespace Services.Implementations;
public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
{
    public async Task<OrderResult> GetOrderByIdAsync(Guid id)
    {
        var order = await _unitOfWork.GetRepository<Order, Guid>()
            .GetByIdAsync(new OrderWithIncludesSpecifications(id))
            ?? throw new OrderNotFoundException(id);
        return _mapper.Map<OrderResult>(order);
    }
    public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
    {
        var orders = await _unitOfWork.GetRepository<Order, Guid>()
            .GetAllAsync(new OrderWithIncludesSpecifications(userEmail));
        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }
    public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
    {
        var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
        return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
    }
    public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
    {
        var address = _mapper.Map<Address>(orderRequest.ShippingAddress);
        var basket = await GetBasketAsync(orderRequest.BasketId);
        var orderItems = await GetOrderItemsAsync(basket);
        var deliveryMethod = await GetMethodAsync(orderRequest.DeliveryMethodId);
        await HandleExistingOrderAsync(basket.PaymentIntentId!);
        var subTotal = CalculateSubTotal(orderItems);
        var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId!);
        await _unitOfWork.GetRepository<Order, Guid>().AddAsync(orderToCreate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OrderResult>(orderToCreate);
    }
    private async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        return await _basketRepository.GetBasketAsync(basketId)
            ?? throw new BasketNotFoundException(basketId);
    }
    private async Task<List<OrderItem>> GetOrderItemsAsync(CustomerBasket basket)
    {
        var orderItems = new List<OrderItem>();
        foreach (var item in basket.BasketItems)
        {
            var product = await _unitOfWork.GetRepository<Product, int>()
                .GetByIdAsync(item.Id)
                ?? throw new ProductNotFoundException(item.Id);
            orderItems.Add(CreateOrderItem(product, item));
        }
        return orderItems;
    }
    private OrderItem CreateOrderItem(Product product, BasketItem item)
    {
        var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
        return new OrderItem(productInOrderItem, product.Price, item.Quantity);
    }
    private async Task<DeliveryMethod> GetMethodAsync(int deliveryMethodId)
    {
        return await _unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetByIdAsync(deliveryMethodId)
            ?? throw new DeliveryMethodNotFoundException(deliveryMethodId);
    }
    public async Task HandleExistingOrderAsync(string paymentIntentId)
    {
        //Check If There Is Order Already Exists Or Not
        var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
        var orderExists = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId!));
        if (orderExists != null)
        {
            orderRepo.Delete(orderExists);
        }
    }
    private decimal CalculateSubTotal(List<OrderItem> orderItems)
    {
        return orderItems.Sum(o => o.Price * o.Quantity);
    }  
}