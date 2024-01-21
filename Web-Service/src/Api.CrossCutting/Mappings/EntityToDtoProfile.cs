using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
                    .ReverseMap();             
            CreateMap<UserDtoCreateResult, UserEntity>()
                .ReverseMap();
            CreateMap<UserDtoUpdateResult, UserEntity>()
                .ReverseMap();            

            CreateMap<UserSettingsDto, UserSettingsEntity>()
                .ReverseMap();


            CreateMap<ProductEntity, ProductDto>()
                .ReverseMap();
            CreateMap<ProductEntity, ProductDtoCreateResult>()
                .ReverseMap();
            CreateMap<ProductEntity, ProductDtoUpdateResult>()
                .ReverseMap();

            CreateMap<ProductEntity, ProductDtoAvailableResult>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Prices.FirstOrDefault(x => x.Current == true)))
                .ReverseMap();

            CreateMap<ProductPriceEntity, ProductPriceDto>()
                .ReverseMap();
            CreateMap<ProductPriceEntity, ProductPriceDtoCreateResult>()
                .ReverseMap();
            CreateMap<ProductPriceEntity, ProductPriceDtoUpdateResult>()
                .ReverseMap();

            CreateMap<PurchaseEntity, PurchaseDto>()
                .ReverseMap();
            CreateMap<PurchaseEntity, PurchaseDtoCreateRequest>()
                .ReverseMap();
            CreateMap<PurchaseEntity, PurchaseDtoUpdateRequest>()
                .ReverseMap();
        }
    }
}