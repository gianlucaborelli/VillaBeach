using Domain.Commands.AuthenticationCommands;
using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Commands.ProductCommands;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureCommandHandlers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Authentication Commands
            services.AddScoped<IRequestHandler<RegisterNewUserCommand, ValidationResult>, RegisterNewUserCommandHandler>();
            services.AddScoped<IRequestHandler<ForgetPasswordRequestCommand, ValidationResult>, ForgetPasswordRequestCommandHandler>();
            services.AddScoped<IRequestHandler<ForgetPasswordVerificationCommand, ValidationResult>, ForgetPasswordVerificationCommandHandler>();

            // User Commands
            services.AddScoped<IRequestHandler<CreateNewUserCommand, ValidationResult>, CreateNewUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, ValidationResult>, DeleteUserCommandHandler>();
            services.AddScoped<IRequestHandler<AddAddressToUserCommand, ValidationResult>, AddAddressToUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserAddressCommand, ValidationResult>, UpdateUserAddressCommandHandler>();

            // Product Commands
            services.AddScoped<IRequestHandler<CreateProductCommand, ValidationResult>, CreateProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, UpdateProductCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, ValidationResult>, DeleteProductCommandHandler>();
        }
    }
}
