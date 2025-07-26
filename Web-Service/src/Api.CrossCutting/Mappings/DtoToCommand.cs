using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Dtos.Authentication;
using Api.Domain.Dtos.User;
using AutoMapper;
using Api.Domain.Commands.PurchaseCommands;
using Api.Domain.Dtos.Purchase;

namespace Api.CrossCutting.Mappings
{
    public class DtoToCommand : Profile
    {
        public DtoToCommand()
        {
            CreateMap<RegisterRequest, RegisterNewUserCommand>()
                .ReverseMap();
            CreateMap<ForgotPasswordRequest, ForgetPasswordRequestCommand>()
                .ReverseMap();
            CreateMap<ForgetPasswordVerificationRequest, ForgetPasswordVerificationCommand>()
                .ReverseMap();

            CreateMap<CreateUserRequest, CreateNewUserCommand>()
                .ReverseMap();
            CreateMap<UpdateUserRequest, UpdateUserCommand>()
                .ReverseMap();
            CreateMap<AddAddressRequest, AddAddressToUserCommand>()
                .ReverseMap();
            CreateMap<UpdateAddressRequest, UpdateUserAddressCommand>()
                .ReverseMap();
            CreateMap<PurchaseDtoCreateRequest, CreatePurchaseCommand>()
                .ForMember(dest => dest.PurchasedProducts, opt => opt.MapFrom(src => src.PurchasedProducts));
            CreateMap<PurchasedProductDtoCreateRequest, CreatePurchaseCommand.PurchasedProductData>();
            CreateMap<PurchaseDtoUpdateRequest, UpdatePurchaseCommand>()
                .ForMember(dest => dest.PurchasedProducts, opt => opt.MapFrom(src => src.PurchasedProducts));
            CreateMap<PurchasedProductDtoCreateRequest, UpdatePurchaseCommand.PurchasedProductData>();
        }
    }
}