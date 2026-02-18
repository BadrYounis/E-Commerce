using AutoMapper;
using Domain.Entities.ProductModule;
using Shared.Dtos;

namespace Services.MappingProfiles;
internal class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductType, TypeResultDto>();
        CreateMap<ProductBrand, BrandResultDto>();
        CreateMap<Product, ProductResultDto>()
            .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name));
    }
}