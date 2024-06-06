using Api.Domain.Dtos.Authentication;
using FluentValidation.Results;

namespace Api.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ValidationResult> Register(RegisterDtoRequest request);

        // Task<ValidationResult> ForgotPasswordRecoveryRequest(string email);

        // Task<ValidationResult> ForgotPasswordRecoveryConfirm(string email);

        // Task<ValidationResult> ChangePasswordRequest(RegisterDtoRequest request);

        // Task<ValidationResult> ChangePasswordConfirm(RegisterDtoRequest request);
    }
}