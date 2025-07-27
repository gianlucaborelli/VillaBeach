using Api.Domain.Dtos.Authentication;
using Api.Service.Interfaces;
using FluentValidation.Results;

namespace Api.Service.Services
{
    public class AuthenticationService() : IAuthenticationService
    {
        

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