using Api.CrossCutting.Identity.Authentication.Model;
using Api.Domain.Dtos.Authentication;
using FluentValidation.Results;

namespace Api.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ValidationResult> Register(RegisterRequest request);
        Task<ValidationResult> ForgetPassword(ForgotPasswordRequest request);
        Task<ValidationResult> ForgetPasswordVerification(ForgetPasswordVerificationRequest requestDto);
    }
}