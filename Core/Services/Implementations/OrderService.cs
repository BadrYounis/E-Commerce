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
    public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
    {
        //1) map address to addressDto
        var address = _mapper.Map<Address>(orderRequest.ShippingAddress);
        //2) getOrderItems => BasketId
        var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId)
            ?? throw new BasketNotFoundException(orderRequest.BasketId);
        var orderItems = new List<OrderItem>();
        foreach (var item in basket.BasketItems)
        {
            var product = await _unitOfWork.GetRepository<Product, int>()
                .GetByIdAsync(item.Id)
                ?? throw new ProductNotFoundException(item.Id);
            orderItems.Add(CreateOrderItem(product, item));
        }
        //3) getDeliveryMethod => DeliveryMethodId
        var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetByIdAsync(orderRequest.DeliveryMethodId)
            ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
        //4) calculate SubTotal
        var subTotal = orderItems.Sum(o => o.Price * o.Quantity);
        //5) create Object From Order
        var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
        await _unitOfWork.GetRepository<Order, Guid>().AddAsync(orderToCreate);
        await _unitOfWork.SaveChangesAsync();
        //6) map <Order, OrderResult>
        return _mapper.Map<OrderResult>(orderToCreate);
    }
    public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
    {
        var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
        return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
    }      
    private OrderItem CreateOrderItem(Product product, BasketItem item)
    {
        var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
        return new OrderItem(productInOrderItem, product.Price, item.Quantity);
    }
}