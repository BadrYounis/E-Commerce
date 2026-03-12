using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts;
public interface IBasketService
{
    Task<BasketDto> GetBasketAsync(string id);
    Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
    Task<bool> DeleteBasketAsync(string id);
}