using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Dtos.Login;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToCommand : Profile
    {
        public DtoToCommand()
        {
            CreateMap<RegisterDtoRequest, RegisterNewUserCommand>()
                .ReverseMap();
            CreateMap<LoginDtoRequest, LoginRequestCommand>()
                .ReverseMap();
        }
    }
}