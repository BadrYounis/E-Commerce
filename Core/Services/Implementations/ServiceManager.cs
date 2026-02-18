using AutoMapper;
using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementations;
public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
{
    private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
    public IProductService ProductService => _productService.Value;
}