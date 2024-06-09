using Api.CrossCutting.Identity.Authentication.Model;
using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Dtos.Authentication;
using Api.Service.Interfaces;
using AutoMapper;
using FluentValidation.Results;
using MediatR;

namespace Api.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IMediator mediator,
            IMapper mapper)
        {        
            _mediator = mediator;
            _mapper = mapper;
        }        

        public Task<ValidationResult> Register(RegisterRequest requestDto)
        {
            var request = _mapper.Map<RegisterNewUserCommand>(requestDto);
            return _mediator.Send(request);
        }

        public Task<ValidationResult> ForgetPasswordRequest(ForgotPasswordRequest user)
        {
            var request =_mapper.Map<ForgetPasswordRequestCommand>(user);
            return _mediator.Send(request);
        }        
    }
}