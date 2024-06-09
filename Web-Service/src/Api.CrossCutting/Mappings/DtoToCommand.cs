using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Dtos.Authentication;
using AutoMapper;

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
        }
    }
}