using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Services.Implementations;
public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
{
    public async Task<BasketDto> GetBasketAsync(string id)
    {
        var basket = await _basketRepository.GetBasketAsync(id);
        return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDto>(basket);
    }
    public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
    {
        var basket = _mapper.Map<CustomerBasket>(basketDto);
        var createdOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
        return createdOrUpdatedBasket is null ? throw new Exception("Can't created or updated basket")
            : _mapper.Map<BasketDto>(createdOrUpdatedBasket);
    }
    public async Task<bool> DeleteBasketAsync(string id)
        => await _basketRepository.DeleteBasketAsync(id);
}