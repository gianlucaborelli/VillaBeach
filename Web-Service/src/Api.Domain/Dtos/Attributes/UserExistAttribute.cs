using System.ComponentModel.DataAnnotations;
using Api.Domain.Interfaces.Services.User;

namespace Api.Domain.Dtos.Attributes
{
    public class UserExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var service = (IUserService?)validationContext
                         .GetService(typeof(IUserService));

            if (service == null)
                throw new InvalidOperationException("IUserService is not registered as a validation service.");            

            return IsValidAsync(value!, service).GetAwaiter().GetResult();
        }

        private async Task<ValidationResult> IsValidAsync(object value, IUserService service)
        {
            var result = await service.Exists((string)value!);
            if (result) return new ValidationResult("User already exists.");

            return ValidationResult.Success!;
        }
    }
}
