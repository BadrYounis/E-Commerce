using AutoMapper;
using Domain.Entities.BasketModule;
using Shared.Dtos.BasketModule;

namespace Services.MappingProfiles;
public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, BasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();
    }
}