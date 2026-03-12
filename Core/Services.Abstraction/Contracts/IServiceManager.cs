namespace Services.Abstraction.Contracts;
public interface IServiceManager
{
    public IProductService ProductService { get; }
    public IBasketService BasketService { get; }
}