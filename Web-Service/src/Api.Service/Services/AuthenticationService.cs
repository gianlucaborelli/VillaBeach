using Api.Core.Mediator;
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
        private readonly IMediatorHandler  _mediator;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IMediatorHandler  mediator,
            IMapper mapper)
        {        
            _mediator = mediator;
            _mapper = mapper;
        }        

        public Task<ValidationResult> Register(RegisterRequest requestDto)
        {
            var request = _mapper.Map<RegisterNewUserCommand>(requestDto);
            return _mediator.SendCommand(request);
        }

        public Task<ValidationResult> ForgetPassword(ForgotPasswordRequest requestDto)
        {
            var request =_mapper.Map<ForgetPasswordRequestCommand>(requestDto);
            return _mediator.SendCommand(request);
        }   

        public Task<ValidationResult> ForgetPasswordVerification(ForgetPasswordVerificationRequest requestDto){
            var request =_mapper.Map<ForgetPasswordVerificationCommand>(requestDto);
            return _mediator.SendCommand(request);
        }     
    }
}