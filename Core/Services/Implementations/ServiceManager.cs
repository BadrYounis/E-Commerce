using AutoMapper;
using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementations;
public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepo) : IServiceManager
{
    private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));

    private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepo, _mapper));
    public IProductService ProductService => _productService.Value;
    public IBasketService BasketService => _basketService.Value;
}