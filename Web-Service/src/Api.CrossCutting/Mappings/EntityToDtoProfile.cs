using Api.Domain.Dtos.Product;
using Api.Domain.Dtos.ProductPrice;
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

            // CreateMap<Product, ProductDtoAvailableResult>()
            //     .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Prices.FirstOrDefault(x => x.Current == true)))
            //     .ReverseMap();

            CreateMap<ProductPrice, ProductPriceDto>()
                .ReverseMap();
            CreateMap<ProductPrice, ProductPriceDtoCreateResult>()
                .ReverseMap();
            CreateMap<ProductPrice, ProductPriceDtoUpdateResult>()
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