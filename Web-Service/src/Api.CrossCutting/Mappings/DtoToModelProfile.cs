using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Product;
using Api.Domain.Dtos.ProductPrice;
using Api.Domain.Dtos.Purchase;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoUpdateRequest>()
                .ReverseMap();

            CreateMap<ProductModel, ProductDto>()
                .ReverseMap();
            CreateMap<ProductModel, ProductDtoCreateRequest>()
                .ReverseMap();
            CreateMap<ProductModel, ProductDtoUpdateRequest>()
                .ReverseMap();

            CreateMap<ProductPriceModel, ProductPriceDto>()
                .ReverseMap();
            CreateMap<ProductPriceModel, ProductPriceDtoCreateRequest>()
                .ReverseMap();
            CreateMap<ProductPriceModel, ProductPriceDtoUpdateRequest>()
                .ReverseMap();

            CreateMap<PurchaseModel, PurchaseDto>()
                .ReverseMap();
            CreateMap<PurchaseModel, PurchaseDtoCreateRequest>()
                .ReverseMap();
            CreateMap<PurchaseModel, PurchaseDtoUpdateRequest>()
                .ReverseMap();
        }
    }
}