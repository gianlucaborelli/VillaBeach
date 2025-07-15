using Api.Domain.Dtos.Product;
using Api.Domain.Dtos.Purchase;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserView, User>()
                .ReverseMap();

            CreateMap<UserAddressDtoCreateRequest, Address>()
                .ReverseMap();
            CreateMap<UserAddressDtoCreateResult, Address>()
                .ReverseMap();
            CreateMap<UserAddressDtoUpdateRequest, Address>()
                .ReverseMap();
            CreateMap<UserAddressDtoUpdateResult, Address>()
                .ReverseMap();

            CreateMap<UserSettingsDto, Settings>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<Product, ProductDtoCreateResult>()
                .ReverseMap();
            CreateMap<Product, ProductDtoUpdateResult>()
                .ReverseMap();

            CreateMap<Product, ProductDtoAvailableResult>()
                .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.Stock > 0))
                .ReverseMap();

            CreateMap<Purchase, PurchaseDto>()
                .ReverseMap();
            CreateMap<Purchase, PurchaseDtoCreateRequest>()
                .ReverseMap();
            CreateMap<Purchase, PurchaseDtoUpdateRequest>()
                .ReverseMap();
        }
    }
}