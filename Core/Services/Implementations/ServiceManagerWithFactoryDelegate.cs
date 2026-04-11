using Services.Abstraction.Contracts;

namespace Services.Implementations;
public class ServiceManagerWithFactoryDelegate(
    Func<IProductService> _productFactory,
    Func<IBasketService> _basketService,
    Func<IAuthenticationService> _authenticationFactory,
    Func<IOrderService> _orderFactory,
    Func<IPaymentService> _paymentFactory) : IServiceManager
{
    public IProductService ProductService => _productFactory.Invoke();
    public IBasketService BasketService => _basketService.Invoke();
    public IAuthenticationService AuthenticationService => _authenticationFactory.Invoke();
    public IOrderService OrderService => _orderFactory.Invoke();
    public IPaymentService PaymentService => _paymentFactory.Invoke();
}