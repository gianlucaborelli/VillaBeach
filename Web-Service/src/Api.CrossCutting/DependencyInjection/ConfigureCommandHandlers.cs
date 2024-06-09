using Api.CrossCutting.Identity.Authentication;
using Api.Domain.Commands.AuthenticationCommands;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureCommandHandlers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<RegisterNewUserCommand, ValidationResult>, AuthenticationCommandsHandler>();
            services.AddScoped<IRequestHandler<ForgetPasswordRequestCommand, ValidationResult>, AuthenticationCommandsHandler>();
        }
    }
}
