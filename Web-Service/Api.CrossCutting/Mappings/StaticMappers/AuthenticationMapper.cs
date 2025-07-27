using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Dtos.Authentication;

namespace Api.CrossCutting.Mappings.StaticMappers
{
    public static class AuthenticationMapper
    {
        public static RegisterNewUserCommand ToRegisterCommand(this RegisterRequest request)
        {
            if (request == null) return null!;

            return new RegisterNewUserCommand
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            };
        }

        public static ForgetPasswordRequestCommand ToForgetPasswordCommand(this ForgotPasswordRequest request)
        {
            if (request == null) return null!;

            return new ForgetPasswordRequestCommand
            {
                Email = request.Email
            };
        }

        public static ForgetPasswordVerificationCommand ToForgetPasswordVerificationCommand(this ForgetPasswordVerificationRequest request)
        {
            if (request == null) return null!;

            return new ForgetPasswordVerificationCommand
            {
                Email = request.Email,
                Token = request.Token,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            };
        }
    }
} 