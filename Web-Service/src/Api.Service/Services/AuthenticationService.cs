using Api.Core.Mediator;
using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Dtos.Authentication;
using Api.Service.Interfaces;
using AutoMapper;
using FluentValidation.Results;

namespace Api.Service.Services
{
    public class AuthenticationService(
        IMediatorHandler mediator,
        IMapper mapper) : IAuthenticationService
    {
        private readonly IMediatorHandler _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task<ValidationResult> Register(RegisterRequest requestDto)
        {
            throw new NotImplementedException("Register method is not implemented yet.");
        }

        public Task<ValidationResult> ForgetPassword(ForgotPasswordRequest requestDto)
        {
            throw new NotImplementedException("ForgetPassword method is not implemented yet.");
        }   

        public Task<ValidationResult> ForgetPasswordVerification(ForgetPasswordVerificationRequest requestDto)
        {
            throw new NotImplementedException("ForgetPasswordVerification method is not implemented yet.");
        }     
    }
}