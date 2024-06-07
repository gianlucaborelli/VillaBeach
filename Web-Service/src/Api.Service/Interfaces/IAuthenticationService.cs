using Api.Domain.Dtos.Authentication;
using FluentValidation.Results;

namespace Api.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ValidationResult> Register(RegisterDtoRequest request);
    }
}