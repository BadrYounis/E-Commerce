using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Shared.Common;

namespace Services.Implementations;
public class ServiceManager(IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IBasketRepository _basketRepo,
    UserManager<User> _userManager,
    IOptions<JwtOptions> _options,
    IConfiguration _configuration) : IServiceManager
{
    private readonly Lazy<IProductService> _productService
        = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));

    private readonly Lazy<IBasketService> _basketService
        = new Lazy<IBasketService>(() => new BasketService(_basketRepo, _mapper));

    private readonly Lazy<IAuthenticationService> _authenticationService
        = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options, _mapper));

    private readonly Lazy<IOrderService> _orderService
        = new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepo, _unitOfWork));

    private readonly Lazy<IPaymentService> _paymentService
        = new Lazy<IPaymentService>(() => new PaymentService(_configuration, _basketRepo, _unitOfWork, _mapper));
    public IProductService ProductService => _productService.Value;
    public IBasketService BasketService => _basketService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public IOrderService OrderService => _orderService.Value;
    public IPaymentService PaymentService => _paymentService.Value;
}